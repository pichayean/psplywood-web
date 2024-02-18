using Microsoft.AspNetCore.Mvc;

namespace PSPlywoodWeb.Controllers
{
    public class UserOnlineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Check()
        {
            return Json(new { success = true });
        }
    }
}
