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
        //private readonly IPSPlywoodService _service;

        //public ProductController(IPSPlywoodService service)
        //{
        //    _service = service;
        //}

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View();
            //return _context.ProductViewModel != null ? 
            //            View(await _context.ProductViewModel.ToListAsync()) :
            //            Problem("Entity set 'PSPlywoodWebContext.ProductViewModel'  is null.");
        }

        //// GET: Product/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.ProductViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var productViewModel = await _context.ProductViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (productViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productViewModel);
        //}

        //// GET: Product/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Product/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id")] ProductViewModel productViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(productViewModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productViewModel);
        //}

        //// GET: Product/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.ProductViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var productViewModel = await _context.ProductViewModel.FindAsync(id);
        //    if (productViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productViewModel);
        //}

        //// POST: Product/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id")] ProductViewModel productViewModel)
        //{
        //    if (id != productViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(productViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductViewModelExists(productViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productViewModel);
        //}

        //// GET: Product/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.ProductViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    var productViewModel = await _context.ProductViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (productViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productViewModel);
        //}

        //// POST: Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.ProductViewModel == null)
        //    {
        //        return Problem("Entity set 'PSPlywoodWebContext.ProductViewModel'  is null.");
        //    }
        //    var productViewModel = await _context.ProductViewModel.FindAsync(id);
        //    if (productViewModel != null)
        //    {
        //        _context.ProductViewModel.Remove(productViewModel);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductViewModelExists(int id)
        //{
        //  return (_context.ProductViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
