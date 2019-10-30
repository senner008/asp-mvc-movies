using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcMovie.Data;
using MvcMovie.Models;

namespace asp_mvc.Controllers
{
    public class MoviesController : Controller {
        private readonly MvcMovieContext _context;

        public ILogger<MoviesController> _logger { get; }

        public MoviesController (MvcMovieContext context, ILogger<MoviesController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: Movies
        public async Task<IActionResult> Index () {
            return View (await _context.Movie.AsNoTracking ().ToListAsync ());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                _logger.LogError($"Id not provided!");
                return View("Error", "Intet id valgt!!");
            }

            var movie = await _context.Movie
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                _logger.LogError($"The movie does not exist!");
                Response.StatusCode = 404;
                return View("Error", "Film med id findes ikke!");
            }
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create () {
            return View ();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Create ([Bind ("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie) {

            if (ModelState.IsValid) {
                _context.Add (movie);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit (int? id) {
            if (id == null) {
                 Response.StatusCode = 404;
                _logger.LogError($"Id not provided!");
                return View("Error", "Intet id valgt!");
            }

            var movie = await _context.Movie.FindAsync (id);
            if (movie == null) {
                 _logger.LogError ($"The movie does not exist!");
                Response.StatusCode = 404;
                return View ("Error", "Film med id findes ikke!");
            }
            return View (movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Edit ([Bind ("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie) {

            if (ModelState.IsValid) {
                try {
                    _context.Update (movie);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!MovieExists (movie.Id)) {
                        _logger.LogError ($"The movie does not exist! Possibly deleted by another admin user");
                        Response.StatusCode = 404;
                        return View ("Error", "Film med id findes ikke! Muligvis slettet af anden admin bruger");
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete (int? id) {
            if (id == null) {
                Response.StatusCode = 404;
                _logger.LogError($"Id not provided!");
                return View("Error", "Intet id valgt!");
            }
            var movie = await _context.Movie
                .FirstOrDefaultAsync (m => m.Id == id);
            if (movie == null) {
                _logger.LogError ($"The movie does not exist!");
                Response.StatusCode = 404;
                return View ("Error", "Film med id findes ikke!");
            }

            return View (movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed (int id) {

            var movie = await _context.Movie.FindAsync (id);

            try {
                _context.Movie.Remove (movie);
                await _context.SaveChangesAsync ();

            } catch (DbUpdateConcurrencyException) {

                if (!MovieExists (movie.Id)) {
                    _logger.LogError ($"The movie does not exist! Possibly deleted by another admin user");
                    Response.StatusCode = 404;
                    return View ("Error", "Film med id findes ikke! Muligvis slettet af anden admin bruger");
                } else {
                    throw;
                }
            }
            return RedirectToAction (nameof (Index));
        }
        private bool MovieExists (int id) {
            return _context.Movie.Any (e => e.Id == id);
        }
    }
}