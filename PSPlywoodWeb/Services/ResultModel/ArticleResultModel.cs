namespace PSPlywoodWeb.Services.ResultModel
{
    public class ArticleResultModel
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string coverUrl { get; set; }
        public string coverImageId { get; set; }
        public string htmlContent { get; set; }
        public string pageUrl { get; set; }
        public string visibility { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public bool iSActive { get; set; }
    }
}
