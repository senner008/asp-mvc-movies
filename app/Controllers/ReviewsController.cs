using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcMovie.Data;
using MvcMovie.Models;

namespace asp_mvc.Controllers {
    public class ReviewsController : Controller {
        private readonly MvcMovieContext _context;

        public ILogger<ReviewsController> _logger { get; }

        public ReviewsController (MvcMovieContext context, ILogger<ReviewsController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: Reviews
        public async Task<IActionResult> Index () {
            return View (await _context.Reviews.Include(r => r.Movie).AsNoTracking().ToListAsync ());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details (int? id) {
            if (id == null) {
                Response.StatusCode = 404;
                _logger.LogError($"Id not provided!");
                return View("Error", "Intet id valgt!!");
            }

            var review = await _context.Reviews
                .Include (r => r.Movie)
                .AsNoTracking ()
                .FirstOrDefaultAsync (m => m.Id == id);
            if (review == null) {
                _logger.LogError($"The review does not exist!");
                Response.StatusCode = 404;
                return View("Error", "Anmeldelse med id findes ikke!");
            }

            return View (review);
        }

        // GET: Reviews/Create
        public IActionResult Create () {
            ViewData["MovieTitle"] = new SelectList (_context.Movie, "Id", "Title");
            return View ();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Create ([Bind ("Id,MovieID,ReviewDate,Article")] Review review) {
            if (ModelState.IsValid) {
                _context.Add (review);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            ViewData["Movie"] = new SelectList (_context.Movie, "Id", "Id", review.MovieID);
            return View (review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit (int? id) {
            if (id == null) {
                return NotFound ();
            }

            var review = await _context.Reviews.FindAsync (id);
            if (review == null) {
                return NotFound ();
            }
            return View (review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,MovieID,ReviewDate,Article")] Review review) {
            if (id != review.Id) {
                return NotFound ();
            }

            var reviewToUpdate = await _context.Reviews
                .FirstOrDefaultAsync (review => review.Id == id);

            if (await TryUpdateModelAsync<Review> (reviewToUpdate,
                    "",
                    c => c.MovieID, c => c.Movie, c => c.Article, c => c.ReviewDate)) {
                try {

                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!ReviewExists (review.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            ViewData["MovieID"] = new SelectList (_context.Movie, "Id", "Id", review.MovieID);
            return View (review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete (int? id) {
            if (id == null) {
                return NotFound ();
            }

            var review = await _context.Reviews
                .Include (r => r.Movie)
                .AsNoTracking ()
                .FirstOrDefaultAsync (m => m.Id == id);
            if (review == null) {
                return NotFound ();
            }

            return View (review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed (int id) {
            var review = await _context.Reviews.FindAsync (id);

            try {
                _context.Reviews.Remove (review);
                await _context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                if (!ReviewExists (review.Id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return RedirectToAction (nameof (Index));
        }

        private bool ReviewExists (int id) {
            return _context.Reviews.Any (e => e.Id == id);
        }
    }
}