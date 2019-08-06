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
    public class CustomersController : Controller
    {
        private readonly MovieRentalContext _context;

        public CustomersController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: Customers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movieRentalContext = _context.Customer;
            return View(await movieRentalContext.ToListAsync());
        }

        // GET: Customers/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Customer customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return BadRequest();
            }

            return View(customer);
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

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,PersonalId,FirstName,FamilyName,Email,PhoneNumber,Address,Gender,Birthday")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Customer.SingleOrDefaultAsync(x => x.PersonalId == customer.PersonalId) == null)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Customer customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CustomerId,PersonalId,FirstName,FamilyName,Email,PhoneNumber,Address,Gender,Birthday")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_context.Loan.AsNoTracking().SingleOrDefault(x => x.CustomerId == customer.CustomerId) != null)
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Customer customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Search(string PersonalId, string FirstName, string FamilyName, string Age)
        {
            IEnumerable<Customer> customers = _context.Customer;

            if (PersonalId != null && PersonalId != string.Empty)
            {
                customers = customers.Where(customer => customer.PersonalId.ToString().ToUpper().Contains(PersonalId.ToUpper()));
            }

            if (FirstName != null && FirstName != string.Empty)
            {
                customers = customers.Where(customer => customer.FirstName.ToUpper().Contains(FirstName.ToUpper()));
            }

            if (FamilyName != null && FamilyName != string.Empty)
            {
                customers = customers.Where(customer => customer.FamilyName.ToUpper().Contains(FamilyName.ToUpper()));
            }

            if (Age != null && Age != string.Empty && int.TryParse(Age, out int age))
            {
                customers = customers.Where(customer => Math.Floor((double)DateTime.Now.Subtract(customer.Birthday).Days / 365) == age);
            }

            return View("Index", customers);
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
