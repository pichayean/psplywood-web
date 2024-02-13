namespace PSPlywoodWeb.Services.ResultModel
{
    public class SettingsResultModel
    {
        public int? id { get; set; }
        public string menuHome { get; set; }
        public string menuProduct { get; set; }
        public string menuContent { get; set; }
        public string menuContactUs { get; set; }
        public string pagraphMain { get; set; }
        public string pagraphMainSmall { get; set; }
        public string pagraphFooter { get; set; }
        public string LogoName { get; set; }
        public string pagraphSectionOne { get; set; }
        public string pagraphSectionOneSmall { get; set; }
        public string pagraphSectionTwo { get; set; }
        public string pagraphSectionTwoSmall { get; set; }
        public string pagraphSectionTree { get; set; }
        public string pagraphSectionTreeSmall { get; set; }
        public string headerColor { get; set; }
        public string footerColor { get; set; }

        public IEnumerable<BannerImageSettingsResultModel> bannerImageSettings { get; set; }
        public IEnumerable<int?> bannerImageSettingsRemove { get; set; }
    }
    public class BannerImageSettingsResultModel
    {
        public string tmpId { get; set; }
        public int? id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string UrlDefault { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string visibility { get; set; }
        public string alt { get; set; }
        public string Base64 { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
