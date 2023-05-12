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
    public class ManagersController : Controller
    {
        private readonly DataDBContext _context;

        public ManagersController(DataDBContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var dataDBContext = _context.Managers.Include(m => m.Company).Include(m => m.Role);
            return View(await dataDBContext.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Company)
                .Include(m => m.Role)
                .FirstOrDefaultAsync(m => m.IdManager == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "IdCompany");
            ViewData["IdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdManager,Name,Email,Password,IdCompany,IdRole")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "IdCompany", manager.IdCompany);
            ViewData["IdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", manager.IdRole);
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "IdCompany", manager.IdCompany);
            ViewData["IdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", manager.IdRole);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdManager,Name,Email,Password,IdCompany,IdRole")] Manager manager)
        {
            if (id != manager.IdManager)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.IdManager))
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
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "IdCompany", manager.IdCompany);
            ViewData["IdRole"] = new SelectList(_context.Roles, "IdRole", "IdRole", manager.IdRole);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Company)
                .Include(m => m.Role)
                .FirstOrDefaultAsync(m => m.IdManager == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Managers == null)
            {
                return Problem("Entity set 'DataDBContext.Managers'  is null.");
            }
            var manager = await _context.Managers.FindAsync(id);
            if (manager != null)
            {
                _context.Managers.Remove(manager);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
          return (_context.Managers?.Any(e => e.IdManager == id)).GetValueOrDefault();
        }
    }
}
