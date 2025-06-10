using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(string SearchText)
        {
            var items = db.Products.Where(x => x.IsActive).ToList();
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText)).ToList();
            }

            var promotionTypes = new Dictionary<int, int?>(); // Tạo từ điển để lưu trữ Promotion Type

            foreach (var item in items)
            {
                var promotionProduct = db.Promotion_Products.SingleOrDefault(x => x.ProductId == item.Id);
                if (promotionProduct != null)
                {
                    promotionTypes[item.Id] = promotionProduct.Type; // Ánh xạ ID sản phẩm với Promotion Type
                }
                else
                {
                    promotionTypes[item.Id] = null; // Nếu không có khuyến mãi, gán Promotion Type thành null
                }
            }
            ViewBag.PromotionTypes = promotionTypes; // Gán từ điển Promotion Types vào ViewBag
            return View(items);
        }

        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            ViewBag.ProductDetail = db.ProductDetails.Where(x => x.ProductId == id);

            var promotionProduct = db.Promotion_Products.SingleOrDefault(x => x.ProductId == id);
            if (promotionProduct != null)
            {
                ViewBag.PromotionType = promotionProduct.Type;
            }

            return View(item);
        }
        public ActionResult ProductCategory(string alias,int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
                ViewBag.CateId = id;
            var promotionTypes = new Dictionary<int, int?>(); // Tạo từ điển để lưu trữ Promotion Type

            foreach (var item in items)
            {
                var promotionProduct = db.Promotion_Products.SingleOrDefault(x => x.ProductId == item.Id);
                if (promotionProduct != null)
                {
                    promotionTypes[item.Id] = promotionProduct.Type; // Ánh xạ ID sản phẩm với Promotion Type
                }
                else
                {
                    promotionTypes[item.Id] = null; // Nếu không có khuyến mãi, gán Promotion Type thành null
                }
            }
            ViewBag.PromotionTypes = promotionTypes; // Gán từ điển Promotion Types vào ViewBag
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            var promotionTypes = new Dictionary<int, int?>(); // Tạo từ điển để lưu trữ Promotion Type

            foreach (var item in items)
            {
                var promotionProduct = db.Promotion_Products.SingleOrDefault(x => x.ProductId == item.Id);
                if (promotionProduct != null)
                {
                    promotionTypes[item.Id] = promotionProduct.Type; // Ánh xạ ID sản phẩm với Promotion Type
                }
                else
                {
                    promotionTypes[item.Id] = null; // Nếu không có khuyến mãi, gán Promotion Type thành null
                }
            }
            ViewBag.PromotionTypes = promotionTypes; // Gán từ điển Promotion Types vào ViewBag
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
    }
}
