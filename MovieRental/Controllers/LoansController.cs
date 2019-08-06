using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.MachineLearning.Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using Microsoft.AspNetCore.Http;

namespace MovieRental.Controllers
{
    public class LoansController : Controller
    {
        private readonly MovieRentalContext _context;

        public LoansController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: Loans
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = new SelectList(_context.Movie, "Name", "Name");
            List<SelectListItem> moviesList = new List<SelectListItem>();
            moviesList.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in movies)
            {
                moviesList.Add(item);
            }

            ViewBag.Name = moviesList;


            var customers = new SelectList(_context.Customer, "PersonalId", "PersonalId");
            List<SelectListItem> customersList = new List<SelectListItem>();
            customersList.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in customers)
            {
                customersList.Add(item);
            }

            ViewBag.PersonalId = customersList;

            var movieRentalContext = _context.Loan.Include(l => l.Customer).Include(l => l.Movie);
            return View(await movieRentalContext.ToListAsync());
        }

        public async Task<string> getRecommendedMovie(string customerId)
        {
            if (customerId == null) throw new ArgumentException("customer id is required!");

            var loans = await _context.Loan
                .Include(l => l.Movie)
                .Include(l => l.Customer)
                .ToListAsync();

            var customers = await _context.Customer.ToListAsync();

            List<int[]> tempDatasset = new List<int[]>();
            foreach (var customer in customers)
            {
                var moviesIdsPerCustomer = loans.Where(l => l.CustomerId == customer.CustomerId)
                    .Select(m => m.MovieId).ToList();

                tempDatasset.Add(moviesIdsPerCustomer.ToArray());
            }

            int[][] dataset = tempDatasset.ToArray();

            Apriori apriori = new Apriori(threshold: 1, confidence: 0);
            AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

            var moviesPerSpecifiedCustomer = loans
                .Where(l => l.Customer.PersonalId == (string)customerId)
                .Select(b => b.MovieId).ToArray();

            int[][] matches = classifier.Decide(moviesPerSpecifiedCustomer);

            if (matches.Length > 0)
            {
                int bestMovieId = matches[0][0];
                string bestMovieName = _context.Movie.Single(m => m.MovieId == bestMovieId).Name;
                return bestMovieName;
            }

            return "There is no recommended movie for this customer.";
        }

        // GET: Loans/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MovieId = new SelectList(_context.Movie, "MovieId", "Name");
            ViewBag.CustomerId = new SelectList(_context.Customer, "CustomerId", "PersonalId");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,CustomerId,LoanDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Loan.SingleOrDefaultAsync(l => l.CustomerId == loan.CustomerId && l.MovieId == loan.MovieId) == null)
                {
                    _context.Add(loan);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.MovieId = new SelectList(_context.Movie, "Id", "Name", loan.MovieId);
            ViewBag.CustomerId = new SelectList(_context.Customer, "Id", "PersonalId", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? movieId, int? customerId, DateTime? loanDate)
        {
            if (movieId == null || customerId == null || loanDate == null)
            {
                return BadRequest();
            }

            Loan loan = await _context.Loan.FindAsync(movieId, customerId, loanDate);
            if (loan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MovieId = new SelectList(_context.Movie, "Id", "Name", loan.MovieId);
            ViewBag.CustomerId = new SelectList(_context.Customer, "Id", "FirstName", loan.CustomerId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MovieId,CustomerId,LoanDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                if (_context.Loan.AsNoTracking().SingleOrDefault(l => l.CustomerId == loan.CustomerId && l.MovieId == loan.MovieId) != null)
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MovieId = new SelectList(_context.Movie, "Id", "Name", loan.MovieId);
            ViewBag.CustomerId = new SelectList(_context.Customer, "Id", "FirstName", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? movieId, int? customerId, DateTime? loanDate)
        {
            if (movieId == null || customerId == null || loanDate == null)
            {
                return NotFound();
            }

            Loan loan = await _context.Loan.Where(l => l.MovieId == movieId && l.CustomerId == customerId && l.LoanDate == loanDate)
                .Include(l => l.Movie)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync();

            if (loan == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int movieId, int customerId, DateTime? loanDate)
        {
            Loan loan = await _context.Loan.FindAsync(movieId, customerId, loanDate);
            await _context.Entry(loan).Reference(l => l.Movie).LoadAsync();
            await _context.Entry(loan).Reference(l => l.Customer).LoadAsync();
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
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

        [HttpPost]
        public ActionResult Search(string Name, string PersonalId, DateTime? LoanDate, DateTime? ReturnDate)
        {
            var movies = new SelectList(_context.Movie, "Name", "Name");
            List<SelectListItem> moviesList = new List<SelectListItem>();
            moviesList.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in movies)
            {
                moviesList.Add(item);
            }

            ViewBag.Name = moviesList;


            var customers = new SelectList(_context.Customer, "PersonalId", "PersonalId");
            List<SelectListItem> customersList = new List<SelectListItem>();
            customersList.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in customers)
            {
                customersList.Add(item);
            }

            ViewBag.PersonalId = customersList;

            IEnumerable<Loan> loans = _context.Loan.Include(b => b.Customer).Include(l => l.Movie);

            if (Name != null && Name != string.Empty)
            {
                loans = loans.Where(loan => loan.Movie.Name == Name);
            }

            if (PersonalId != null && PersonalId != string.Empty)
            {
                loans = loans.Where(loan => loan.Customer.PersonalId.ToString() == PersonalId);
            }

            if (LoanDate != null && LoanDate.HasValue)
            {
                loans = loans.Where(loan => loan.LoanDate.Equals(LoanDate));
            }

            if (ReturnDate != null && ReturnDate.HasValue)
            {
                loans = loans.Where(loan => loan.ReturnDate.Equals(ReturnDate));
            }

            return View("Index", loans);
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
