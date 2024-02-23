using PSPlywoodWeb.Services.ResultModel;

namespace PSPlywoodWeb.Models
{
    public class LayoutViewModel
    {
        public ContactUsResultModel Contact { get; set; }
        public SettingsResultModel Setting { get; set; }
        public int UserOnlineCnt { get; set; }
    }
}
