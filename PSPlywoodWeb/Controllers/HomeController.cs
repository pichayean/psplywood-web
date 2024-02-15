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
            var products = await _psPlywoodService.GetProductsAsync(0);
            var settings = await _psPlywoodService.GetSettingsAsync();
            var contact = await _psPlywoodService.GetContactUsAsync();
            if (products.Any() && products.Count < 6)
            {
                foreach (var item in products)
                {
                    item.productName = item.productName.Length > 30 ? item.productName.Substring(0, 30) + "..." : item.productName;
                }
                var cnt = 6 - products.Count;
                for (var i = 0; i < cnt; i++)
                {
                    products.Add(products.FirstOrDefault());
                }
            }
            return View(new ProductViewModel
            {
                Categories = categories,
                Products = products,
                Contact = contact,
                Settings = settings
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}