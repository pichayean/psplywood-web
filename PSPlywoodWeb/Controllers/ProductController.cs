using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PSPlywoodWeb.Models.Products;
using PSPlywoodWeb.Services;
using PSPlywoodWeb.Services.ResultModel;

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
            var tags = await _service.GetAllTagsAsync();
            var products = await _service.GetAllProductsAsync(-1);
            var categories = await _service.GetCategoriesAsync();
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
            return View(new ProductListViewModel
            {
                Categories = categories,
                Products = products,
                Tags = tags
            });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("index");

            var product = await _service.GetProductDetailAsync((int)id);
            if (product != null)
            {
                if (product.productPrices?.Any()??false)
                {
                    product.productPrices = product.productPrices.OrderBy(_ => _.price).ToList();
                }
            }

            var categories = await _service.GetCategoriesAsync();
            return View(new ProductDetailViewModel
            {
                Categorie = categories?.FirstOrDefault(_=>_.id == product.categoryId)??new CategoryResultModel(),
                Product = product,
            });
        }
        public async Task<IActionResult> Order(ProductDetailViewModel model)
        {
            await _service.SendMessage(model.mobileNo, model.productId);
            return RedirectToAction("AlertMessage", "Product");
        }
        public async Task<IActionResult> AlertMessage()
        {
            return View();
        }
        
    }
}
