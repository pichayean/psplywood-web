using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<OnlineUsersHub> _hubContext;
        
        public HomeController(IPSPlywoodService psPlywoodService
            , ILogger<HomeController> logger
            , IHubContext<OnlineUsersHub> hubContext)
        {
            _psPlywoodService = psPlywoodService;
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var userName = $"psplywood_{Guid.NewGuid().ToString()}";
            // Notify other clients that this user is connected
            await _hubContext.Clients.All.SendAsync("UserConnected", userName);

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
            var categoriesStr = "";
            var comma = "";
            foreach (var item in categories)
            {
                categoriesStr += comma + item.categoryName;
                comma = " | ";
            }
            return View(new ProductViewModel
            {
                Categories = categories,
                Products = products,
                Contact = contact,
                categoriesStr = categoriesStr,
                Settings = settings
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
        // public async Task<IActionResult> OnDisconnected()
        // {
        //     // Get the current username
        //     var username = "CurrentUser";

        //     // Notify other clients that this user is disconnected
        //     await _hubContext.Clients.All.SendAsync("UserDisconnected", username);

        //     return View();
        // }
    }
}