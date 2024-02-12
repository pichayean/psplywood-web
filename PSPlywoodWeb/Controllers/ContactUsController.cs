using Microsoft.AspNetCore.Mvc;

namespace PSPlywoodWeb.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
