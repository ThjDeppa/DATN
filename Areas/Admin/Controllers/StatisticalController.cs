using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Statistical
        public ActionResult Index()
        {
            var doanhThu = db.Orders
                .Where(x => x.Status == 3)  // Đơn hàng đã hoàn thành
                .Sum(x => (decimal?)x.TotalAmount) ?? 0;

            ViewBag.DoanhThu = doanhThu;

            return View();
        }

        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on o.Id equals od.OrderId
                        where o.Status == 3 // chỉ tính đơn đã hoàn thành
                        select new
                        {
                            o.CreatedDate,
                            od.Quantity,
                            od.Price
                        };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.CreatedDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                query = query.Where(x => x.CreatedDate < endDate);
            }

            var result = query
                .GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate))
                .Select(g => new
                {
                    Date = g.Key.Value,
                    DoanhThu = g.Sum(x => x.Quantity * x.Price)
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}
