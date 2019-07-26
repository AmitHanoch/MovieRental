using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.MachineLearning.Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;

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
        public async Task<IActionResult> Index()
        {
            var movieRentalContext = _context.Loan.Include(l => l.Customer);
            return View(await movieRentalContext.ToListAsync());
        }

        public async Task<string> getRecommendedBook(string customerId)
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
                var moviesIdsPerCustomer = loans.Where(l => l.CustomerId == customer.Id)
                    .Select(m => m.MovieId).ToList();

                tempDatasset.Add(moviesIdsPerCustomer.ToArray());
            }

            int[][] dataset = tempDatasset.ToArray();

            Apriori apriori = new Apriori(threshold: 1, confidence: 0);
            AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

            var moviesPerSpecifiedCustomer = loans
                .Where(l => l.Customer.PersonalID == (string)customerId)
                .Select(b => b.MovieId).ToArray();

            int[][] matches = classifier.Decide(moviesPerSpecifiedCustomer);

            if (matches.Length > 0)
            {
                int bestMovieId = matches[0][0];
                string bestMovieName = _context.Movie.Single(m => m.Id == bestMovieId).Name;
                return bestMovieName;
            }

            return "There is no recommended movie for this customer.";
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Address");
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
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Address", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Address", loan.CustomerId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,CustomerId,LoanDate,ReturnDate")] Loan loan)
        {
            if (id != loan.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.MovieId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Address", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.MovieId == id);
        }
    }
}
