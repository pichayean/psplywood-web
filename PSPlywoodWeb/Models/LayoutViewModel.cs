using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Models
{
    public class LayoutViewModel
    {
        public int SiteVisitCounter { get; set; }
        public ContactUsResultModel Contact { get; set; }
        public SettingsResultModel Setting { get; set; }
    }
}
