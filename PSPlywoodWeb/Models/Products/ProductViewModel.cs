using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Models.Products
{
    public class ProductViewModel
    {
        public List<CategoryResultModel> Categories { get; set; }
        public List<ProductResultModel> Products { get; set; }
        public SettingsResultModel Settings { get; set; }
        public String categoriesStr { get; set; }
        public ContactUsResultModel Contact { get; set; }
        public string CurrentDate { get; set; }

    }
}
