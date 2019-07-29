using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using Newtonsoft.Json;

namespace MovieRental.Controllers
{
    public class BranchesController : Controller
    {
        private readonly MovieRentalContext _context;

        public BranchesController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: Branches
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branch.ToListAsync());
        }

        public async Task<string> GetBranches()
        {
            List<Branch> branches = await _context.Branch.ToListAsync();

            if(branches.Count() == 0)
            {
                return "";
            }

            return JsonConvert.SerializeObject(branches);
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
