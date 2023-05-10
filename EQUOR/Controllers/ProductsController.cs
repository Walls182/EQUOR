using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQUOR.DataContext;
using EQUOR.Models;
using Microsoft.CodeAnalysis;

namespace EQUOR.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataDBContext _context;

        public ProductsController(DataDBContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int productId)
        {
          
            var dataDBContext = _context.Products.Include(p => p.Manager);
            return View(await dataDBContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    



        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdManager"] = new SelectList(_context.Set<Manager>(), "IdManager", "IdManager");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,IdManager,Name,DesProduct,TipeTransport,QWaterUsed,QEnergy,QWaste,CodigoQR,CarbonFootprint,TimeSearch")] Product product)
        {

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                // Buscar el producto en la base de datos
                

                // Calcular la huella de carbono del producto
                var carbonFootprint = product.CalculateCarbonFootprint(product.TipeTransport,
                                                               product.QWaterUsed,
                                                               product.QEnergy,
                                                               product.QWaste);

                // Actualizar el valor de la huella de carbono en la base de datos
                product.CarbonFootprint = carbonFootprint;
                _context.SaveChanges();

                // Retornar la huella de carbono calculada
                return Ok(carbonFootprint);

            }

            ViewData["IdManager"] = new SelectList(_context.Set<Manager>(), "IdManager", "IdManager", product.IdManager);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdManager"] = new SelectList(_context.Set<Manager>(), "IdManager", "IdManager", product.IdManager);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,IdManager,Name,DesProduct,TipeTransport,QWaterUsed,QEnergy,QWaste,CodigoQR,CarbonFootprint,TimeSearch")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdManager"] = new SelectList(_context.Set<Manager>(), "IdManager", "IdManager", product.IdManager);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DataDBContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
    }
}
