namespace PSPlywoodWeb.Services.ResultModel
{
    public class ProductResultModel
    {
        public int? id { get; set; }
        public string productName { get; set; }
        public string coverUrl { get; set; }
        public string productDetail { get; set; }
        public int? categoryId { get; set; }
        public System.DateTime? createDate { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public Nullable<bool> IsBestSaller { get; set; }
        public string tag { get; set; }
        public string visibility { get; set; }
        public string pageUrl { get; set; }
        public IEnumerable<ImageProductsResultModel> imageProducts { get; set; }
        public IEnumerable<ImageProductsResultModel> imageProductsRemove { get; set; }
        public string imageBase64 { get; set; }
        public string categoryName { get; set; }
        public List<ProductPriceResultModel> productPrices { get; set; }

        public int totalReview { get; set; }
        public int totalImage { get; set; }
    }

    public partial class ImageProductsResultModel
    {
        public int? id { get; set; }
        public Nullable<int> productId { get; set; }
        public string imageProduct { get; set; }
        public string url { get; set; }

        public string src { get; set; }
        public string w { get; set; }
        public string h { get; set; }
    }

    public partial class ProductPriceResultModel
    {
        public int? id { get; set; }
        public int? productId { get; set; }
        public string Size { get; set; }
        public decimal price { get; set; }
        public string remark { get; set; }
        public int minimum { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<bool> isEdit { get; set; }
    }
}
