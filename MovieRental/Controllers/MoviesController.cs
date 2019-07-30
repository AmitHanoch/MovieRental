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
            return View(await _context.Movie.Include(movie => movie.Genre).Include(movie => movie.Producer).ToListAsync());
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

            Movie movieQueried = await _context.Movie.Where(movie => movie.Id == id)
                .Include(movie => movie.Genre).Include(movie => movie.Producer)
                .FirstOrDefaultAsync();

            if (movieQueried == null)
            {
                return NotFound();
            }

            return View(movieQueried);
        }

        public async Task<List<Movie>> TopFiveLoanedMovies()
        {
            var join_loans_movies_query = from movie in _context.Movie
                                          join loan in _context.Loan on movie.Id equals loan.MovieId
                                          select new
                                          {
                                              movieName = movie.Name
                                          };

            var groupBy = join_loans_movies_query.GroupBy(res => res.movieName);

            return new List<Movie>();
        }

        // GET: Movies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Movie movietoEdit = await _context.Movie.FindAsync(id);
            if (movietoEdit == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "Id", "Name", movietoEdit.GenreId);
            return View(movietoEdit);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,ReleaseDate,ProducerId,Price,GenreId")] Movie movieToEdit)
        {
            if (ModelState.IsValid)
            {
                if (_context.Movie.AsNoTracking().SingleOrDefault(x => x.Id == movieToEdit.Id) != null)
                {
                    _context.Entry(movieToEdit).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "Id", "Name", movieToEdit.GenreId);

            return View(movieToEdit);
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
