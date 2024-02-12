using Microsoft.AspNetCore.Mvc;

namespace PSPlywoodWeb.Controllers
{
    public class CartListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
