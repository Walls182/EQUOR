
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQUOR.DataContext;
using EQUOR.Models;

namespace EQUOR.Controllers
{
    public class OpinionsController : Controller
    {
        private readonly DataDBContext _context;

        public OpinionsController(DataDBContext context)
        {
            _context = context;
        }

        // GET: Opinions
        public async Task<IActionResult> Index()
        {
            var dataDBContext = _context.Opinions.Include(o => o.Consumer).Include(o => o.Product);
            return View(await dataDBContext.ToListAsync());
        }

        // GET: Opinions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Opinions == null)
            {
                return NotFound();
            }

            var opinions = await _context.Opinions
                .Include(o => o.Consumer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOpinion == id);
            if (opinions == null)
            {
                return NotFound();
            }

            return View(opinions);
        }

        // GET: Opinions/Create
        public IActionResult Create()
        {
            ViewData["IdConsumer"] = new SelectList(_context.Consumers, "IdConsumer", "IdConsumer");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct");
            return View();
        }

        // POST: Opinions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOpinion,Date,IdConsumer,IdProduct,Favorite")] Opinions opinions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConsumer"] = new SelectList(_context.Consumers, "IdConsumer", "IdConsumer", opinions.IdConsumer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", opinions.IdProduct);
            return View(opinions);
        }

        // GET: Opinions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Opinions == null)
            {
                return NotFound();
            }

            var opinions = await _context.Opinions.FindAsync(id);
            if (opinions == null)
            {
                return NotFound();
            }
            ViewData["IdConsumer"] = new SelectList(_context.Consumers, "IdConsumer", "IdConsumer", opinions.IdConsumer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", opinions.IdProduct);
            return View(opinions);
        }

        // POST: Opinions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOpinion,Date,IdConsumer,IdProduct,Favorite")] Opinions opinions)
        {
            if (id != opinions.IdOpinion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinionsExists(opinions.IdOpinion))
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
            ViewData["IdConsumer"] = new SelectList(_context.Consumers, "IdConsumer", "IdConsumer", opinions.IdConsumer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", opinions.IdProduct);
            return View(opinions);
        }

        // GET: Opinions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Opinions == null)
            {
                return NotFound();
            }

            var opinions = await _context.Opinions
                .Include(o => o.Consumer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.IdOpinion == id);
            if (opinions == null)
            {
                return NotFound();
            }

            return View(opinions);
        }

        // POST: Opinions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Opinions == null)
            {
                return Problem("Entity set 'DataDBContext.Opinions'  is null.");
            }
            var opinions = await _context.Opinions.FindAsync(id);
            if (opinions != null)
            {
                _context.Opinions.Remove(opinions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinionsExists(int id)
        {
          return (_context.Opinions?.Any(e => e.IdOpinion == id)).GetValueOrDefault();
        }
    }
}
