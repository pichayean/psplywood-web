using Microsoft.AspNetCore.Mvc;

namespace PSPlywoodWeb.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
