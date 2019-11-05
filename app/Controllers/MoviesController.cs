using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace asp_mvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Error")]
        public IActionResult Error(string msg = "Not found") {
            Response.StatusCode = 404;
            return View("Error", msg);
        } 

        // GET: Movies
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index () {
            return View (await _context.Movie.AsNoTracking ().ToListAsync ());
        }
        
        // GET: Movies/Details/5
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return Error("Please provide id!");
            }

            var movie = await _context.Movie
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return Error("Movie could not be found");
            }
            return View(movie);
        }
        
        // GET: Movies/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

         
        // GET: Movies/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return Error("Please provide id!");
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return Error("Movie could not be found");
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Movie movie)
        {
            if (id != movie.Id)
            {
                return Error("Please provide id!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return Error("Movie could not be found. Might have been deleted by another user");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return Error("Movie could not be found");
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
        {
             var movie = await _context.Movie.FindAsync(id);

            try {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();

            } catch (DbUpdateConcurrencyException) {
                 if (!MovieExists(movie.Id))
                    {
                        return Error("Movie could not be found. Might have been deleted by another user");
                    }
                    else
                    {
                        throw;
                    }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
