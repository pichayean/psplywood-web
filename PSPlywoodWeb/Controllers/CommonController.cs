using Microsoft.AspNetCore.Mvc;
using PSPlywoodWeb.Services;

namespace PSPlywoodWeb.Controllers
{
    public class CommonController : Controller
    {
        private readonly IPSPlywoodService _psPlywoodService;
        
        public CommonController(IPSPlywoodService psPlywoodService)
        {
            _psPlywoodService = psPlywoodService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SetUserVisit()
        {
            _psPlywoodService.SetSiteVisitCounter();
            return Json(new { success = true });
        }
    }
}
