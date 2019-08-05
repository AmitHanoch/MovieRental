using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRentalContext _context;

        public MoviesController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<ActionResult> Index()
        {
            return View(await _context.Movie.Include(movie => movie.Genre).ToListAsync());
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            User authUser = await _context.User.SingleOrDefaultAsync(user => user.Username == userName &&
                                                     user.Password == password);

            if (authUser != null)
            {
                HttpContext.Session.SetInt32("LoggedIn", authUser.RoleId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: Movie/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Movie movieQueried = await _context.Movie.Where(movie => movie.MovieId == id)
                .Include(movie => movie.Genre)
                .FirstOrDefaultAsync();

            if (movieQueried == null)
            {
                return NotFound();
            }

            return View(movieQueried);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Movie movieToEdit = await _context.Movie.FindAsync(id);
            if (movieToEdit == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name", movieToEdit.GenreId);
            return View(movieToEdit);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MovieId,Name,ReleaseDate,Producer,GenreId,Price")] Movie movieToEdit)
        {
            if (ModelState.IsValid)
            {
                if (_context.Movie.AsNoTracking().SingleOrDefault(x => x.MovieId == movieToEdit.MovieId) != null)
                {
                    _context.Entry(movieToEdit).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name", movieToEdit.GenreId);
            return View(movieToEdit);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Movie movieToDelete = await _context.Movie.Where(movie => movie.MovieId == id)
                .Include(movie => movie.Genre)
                .FirstOrDefaultAsync();

            if (movieToDelete == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(movieToDelete);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Movie movieToDelete = await _context.Movie.Where(movie => movie.MovieId == id)
               .Include(movie => movie.Genre)
               .FirstOrDefaultAsync();

            _context.Movie.Remove(movieToDelete);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Name,ReleaseDate,Producer,GenreId,Price")] Movie movieToAdd)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Movie.SingleOrDefaultAsync(l => l.MovieId == movieToAdd.MovieId) == null)
                {
                    _context.Add(movieToAdd);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name", movieToAdd.GenreId);
            return View(movieToAdd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
