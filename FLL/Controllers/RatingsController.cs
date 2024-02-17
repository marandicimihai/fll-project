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
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rating.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Guid exhibitId, Guid userId, double ratingValue)
        {
            if (ModelState.IsValid)
            {
                var rating = await _context.Rating.FirstOrDefaultAsync(r => r.UserId == userId);

                if (rating is null)
                {
                    rating = new Rating()
                    {
                        RatingValue = ratingValue,
                        ExhibitId = exhibitId,
                        UserId = userId
                    };
                    _context.Add(rating);
                }
                else
                {
                    rating.ExhibitId = exhibitId;
                    rating.RatingValue = ratingValue;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("LeaveMessage", rating);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LeaveMessage(Rating rating)
        {
            return View(rating);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateRatingMessage(Guid ratingId, Rating ratingForm)
        {
            var rating = await _context.Rating.FirstOrDefaultAsync(r => r.RatingId == ratingId);

            if (rating is not null)
            {
                rating.Message = ratingForm.Message;
            }

            await _context.SaveChangesAsync();

            return View("ThankYou");
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RatingId,UserId,ExhibitId,RatingValue,Message")] Rating rating)
        {
            if (id != rating.RatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingId))
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
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(Guid id)
        {
            return _context.Rating.Any(e => e.RatingId == id);
        }
    }
}
