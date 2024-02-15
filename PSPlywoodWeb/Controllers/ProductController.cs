using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PSPlywoodWeb.Models.Products;
using PSPlywoodWeb.Services;

namespace PSPlywoodWeb.Controllers
{
    public class ProductController : Controller
    {
        private IPSPlywoodService _service;

        public ProductController(IPSPlywoodService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetProductsAsync(1);
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }
        public async Task<IActionResult> Order(int? productId)
        {
            await _service.SendMessage("085-5254-556", productId?.ToString());
            return RedirectToAction("AlertMessage", "Product");
        }
        public async Task<IActionResult> AlertMessage()
        {
            return View();
        }
        
    }
}
