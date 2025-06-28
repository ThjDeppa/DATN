using System.Collections.Generic;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models.ViewModels
{
    public class PromotionViewModel
    {
        public List<Promotion_Product> PromotionProducts { get; set; }
        public List<Vouchers> Vouchers { get; set; }
    }
}
