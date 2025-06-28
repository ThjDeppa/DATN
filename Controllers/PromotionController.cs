using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ViewModels;
using System.Data.Entity;

namespace WebBanHangOnline.Controllers
{
    public class PromotionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var view = new PromotionViewModel
            {
                PromotionProducts = db.Promotion_Products
                    .Include(p => p.Product)
                    .Include(p => p.Promotion)
                    .ToList(),
                Vouchers = db.Vouchers.ToList()
            };

            return View(view);
        }
    }
}
