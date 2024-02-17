using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FLL.Data;
using FLL.Models;
using Microsoft.AspNetCore.Authorization;

namespace FLL.Controllers
{
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExhibitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exhibits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exhibit.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Exhibits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit
                .Include(e => e.Ratings)
                .FirstOrDefaultAsync(m => m.ExhibitId == id);
            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }

        // GET: Exhibits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exhibits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExhibitName,ExhibitDescription,PhotoUrl1,PhotoUrl2")] Exhibit exhibit)
        {
            exhibit.ExhibitId = Guid.NewGuid();
            _context.Add(exhibit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Exhibits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit.FindAsync(id);
            if (exhibit == null)
            {
                return NotFound();
            }
            return View(exhibit);
        }

        // POST: Exhibits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExhibitId,ExhibitName,ExhibitDescription,PhotoUrl1,PhotoUrl2")] Exhibit exhibit)
        {
            if (id != exhibit.ExhibitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exhibit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhibitExists(exhibit.ExhibitId))
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
            return View(exhibit);
        }

        // GET: Exhibits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhibit = await _context.Exhibit
                .FirstOrDefaultAsync(m => m.ExhibitId == id);
            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }

        // POST: Exhibits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exhibit = await _context.Exhibit.FindAsync(id);
            if (exhibit != null)
            {
                _context.Exhibit.Remove(exhibit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExhibitExists(Guid id)
        {
            return _context.Exhibit.Any(e => e.ExhibitId == id);
        }
    }
}
