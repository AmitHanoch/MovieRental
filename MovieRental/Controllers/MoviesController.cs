using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private List<Movie> _moviesFromDB = new List<Movie> {
            new Movie
            {
                MovieId = 1,
                Name = "The Green Mile",
                ReleaseDate = new DateTime(2000, 2, 18),
                ProducerId = 1,
                GenreId = 3
            },
            new Movie
            {
                MovieId = 2,
                Name = "The Green Mile",
                ReleaseDate = new DateTime(2000, 2, 18),
                ProducerId = 1,
                GenreId = 3
            },
            new Movie
            {
                MovieId = 3,
                Name = "The Green Mile",
                ReleaseDate = new DateTime(2000, 2, 18),
                ProducerId = 1,
                GenreId = 3
            },
            new Movie
            {
                MovieId = 4,
                Name = "The Green Mile",
                ReleaseDate = new DateTime(2000, 2, 18),
                ProducerId = 1,
                GenreId = 3
            },
            new Movie
            {
                MovieId = 5,
                Name = "The Green Mile",
                ReleaseDate = new DateTime(2000, 2, 18),
                ProducerId = 1,
                GenreId = 3
            }
        };

        public MoviesController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: Movies
        public ActionResult Index()
        {
            return View(_moviesFromDB);
        }

        public ActionResult Login()
        {
            return View();
        }

      // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
           return View(_moviesFromDB[0]);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            //ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        //// POST: Movie/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Author,ReleaseDate,GenreId")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Books.Add(book);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        //// GET: Books/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Author,ReleaseDate,GenreId")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (db.Books.AsNoTracking().SingleOrDefault(x => x.Id == book.Id) != null)
        //        {
        //            db.Entry(book).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
        //    return View(book);
        //}

        //// GET: Books/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Where(x => x.Id == id).Include(x => x.Genre).FirstOrDefault();
        //    if (book == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(book);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Book book = db.Books.Find(id);
        //    db.Books.Remove(book);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Search(string Id, string BookName, string Author, string GenreId, DateTime? ReleaseDate)
        //{
        //    var genres = new SelectList(db.Genres, "Name", "Name");
        //    List<SelectListItem> genresList = new List<SelectListItem>();
        //    genresList.Add(new SelectListItem { Text = "", Value = "" });
        //    foreach (var item in genres)
        //    {
        //        genresList.Add(item);
        //    }
        //    ViewBag.GenreId = genresList;
        //    IEnumerable<Book> books = db.Books.Include(b => b.Genre);

        //    if (Id != string.Empty)
        //    {
        //        books = books.Where(book => book.Id.ToString() == Id);
        //    }

        //    if (BookName != string.Empty)
        //    {
        //        books = books.Where(book => book.Name.Contains(BookName));
        //    }

        //    if (Author != string.Empty)
        //    {
        //        books = books.Where(book => book.Author.Contains(Author));
        //    }

        //    if (GenreId != string.Empty)
        //    {
        //        books = books.Where(book => book.Genre.Name == GenreId);
        //    }

        //    if (ReleaseDate.HasValue)
        //    {
        //        books = books.Where(book => book.ReleaseDate.Equals(ReleaseDate));
        //    }

        //    return View("Index", books);
        //}

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
