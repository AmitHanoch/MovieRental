
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRental.Controllers
{
    public class HomeController : Controller
    {
        private const int NUMBER_IN_LIST = 5;
        private readonly MovieRentalContext _context;

        public HomeController(MovieRentalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NewReleases = await this.GetNewRealeasesForList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<Movie>> GetNewRealeasesForList()
        {
            return await _context.Movie
                .OrderByDescending(movie => movie.ReleaseDate)
                .Take(NUMBER_IN_LIST)
                .Include(movie => movie.Genre)
                .ToListAsync();
        }
        private async Task<List<Movie>> TopFiveLoanedMovies()
        {
            var join_loans_movies_query = from movie in _context.Movie
                                          join loan in _context.Loan on movie.MovieId equals loan.MovieId
                                          select new
                                          {
                                              movieName = movie.Name
                                          };

            var groupBy = join_loans_movies_query.GroupBy(res => res.movieName);

            return new List<Movie>();
        }
    }
}
