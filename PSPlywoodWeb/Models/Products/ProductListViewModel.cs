using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Models.Products
{
    public class ProductListViewModel
    {
        public List<CategoryResultModel> Categories { get; set; }
        public List<ProductResultModel> Products { get; set; }
        public List<string> Tags { get; set; }
    }
}
