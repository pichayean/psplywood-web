using Microsoft.AspNetCore.Mvc;
using PSPlywoodWeb.Models;
using PSPlywoodWeb.Services;

namespace PSPlywoodWeb.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IPSPlywoodService _psPlywoodService;

        public ContactUsController(IPSPlywoodService psPlywoodService)
        {
            _psPlywoodService = psPlywoodService;
        }
        public async Task<IActionResult> Index()
        {
            var contact = await _psPlywoodService.GetContactUsAsync();
            ViewData["SS"] = "ss";
            return View(new ContactUsViewModel
            {
                Contact = contact
            });
        }
    }
}
