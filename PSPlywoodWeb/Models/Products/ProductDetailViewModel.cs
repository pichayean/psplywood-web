using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Models.Products
{
    public class ProductDetailViewModel
    {
        public CategoryResultModel Categorie { get; set; }
        public ProductResultModel Product { get; set; }
        public string mobileNo { get; set; }
        public string productId { get; set; }
    }
}
