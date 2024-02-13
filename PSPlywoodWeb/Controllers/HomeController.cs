using Microsoft.AspNetCore.Mvc;
using PSPlywoodWeb.Models;
using PSPlywoodWeb.Models.Products;
using PSPlywoodWeb.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace PSPlywoodWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPSPlywoodService _psPlywoodService;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(IPSPlywoodService psPlywoodService, ILogger<HomeController> logger)
        {
            _psPlywoodService = psPlywoodService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _psPlywoodService.GetCategoriesAsync();
            return View(new ProductViewModel
            {
                Categories = categories,
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}