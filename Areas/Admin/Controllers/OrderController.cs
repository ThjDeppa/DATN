using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;
using System.Globalization;
using System.Data.Entity;
using WebBanHangOnline.Models.ViewModels;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        public JsonResult GetStatus(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null) return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                Success = true,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult UpdateTT(int id, int trangthai, int paymentStatus)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return Json(new { Success = false, Message = "Đơn hàng không tồn tại" });
            }

            int currentStatus = order.Status;

            bool canUpdate = false;

            switch (currentStatus)
            {
                case -1: // Chờ xác nhận
                case 2:  // Đã xác nhận
                         // Có thể chuyển sang Đã xác nhận (2), Đang giao (3) hoặc Hủy (0)
                    if (trangthai == 2 || trangthai == 3 || trangthai == 0)
                        canUpdate = true;
                    break;

                case 3: // Đang giao
                        // Có thể chuyển sang Đã giao (1) hoặc Hủy (0)
                    if (trangthai == 1 || trangthai == 0)
                        canUpdate = true;
                    break;

                case 1: // Đã giao
                case 0: // Hủy
                        // Không được phép cập nhật nữa
                    canUpdate = false;
                    break;

                default:
                    canUpdate = false;
                    break;
            }

            if (!canUpdate)
            {
                return Json(new { Success = false, Message = "Trạng thái không hợp lệ hoặc không thể cập nhật." });
            }

            // Cập nhật trạng thái
            if (trangthai == 0 && currentStatus != 0)
            {
                foreach (var item in order.OrderDetails)
                {
                    // Giả sử bạn có bảng ProductDetails chứa số lượng size cụ thể
                    var productDetail = db.ProductDetails.FirstOrDefault(p => p.Id == item.ProductDetailId);
                    if (productDetail != null)
                    {
                        productDetail.Quantity += item.Quantity;
                    }
                }
            }
            order.Status = trangthai;
            order.PaymentStatus = paymentStatus;
            db.SaveChanges();

            return Json(new { Success = true });

        }



        public void ThongKe(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on o.Id equals od.OrderId
                        join p in db.ProductDetails on od.ProductDetailId equals p.Id
                        select new
                        {
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.Product.Price
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime start = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.CreatedDate >= start);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.CreatedDate < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(r => new
            {
                Date = r.Key.Value,
                TotalBuy = r.Sum(x => x.OriginalPrice * x.Quantity), // tổng giá bán
                TotalSell = r.Sum(x => x.Price * x.Quantity) // tổng giá mua
            }).Select(x => new RevenueStatisticViewModel
            {
                Date = x.Date,
                
                Revenues = x.TotalSell
            });
        }
    }
}