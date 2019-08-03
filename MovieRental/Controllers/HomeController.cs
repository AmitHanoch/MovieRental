
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using Newtonsoft.Json.Linq;
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

        public JsonResult GetMostPopularForList()
        {
            List<JObject> returnObject = new List<JObject>();

            //select top 5 m.Name, loans.NumberOfLoans from Movie m,
            // (select l.MovieId, COUNT(l.MovieId) as NumberOfLoans from Loan l group by l.MovieId) loans
            // where m.MovieId = loans.MovieId
            // order by loans.NumberOfLoans DESC
            var query = from m in _context.Movie
                        select new
                        {
                            Name = m.Name,
                            MovieId = m.MovieId,
                            NumberOfLoans = (from loans in _context.Loan
                                             group loans by loans.MovieId into sub
                                             select new
                                             {
                                                 MovieId = sub.Key,
                                                 NumberOfLoans = sub.Count()
                                             })
                        };

            foreach (var item in query)
            {
                JObject obj = new JObject();
                obj["Name"] = item.Name;
                obj["MovieId"] = item.MovieId;

                foreach (var loanCount in item.NumberOfLoans)
                {
                    if(loanCount.MovieId == item.MovieId)
                    {
                        obj["NumberOfLoans"] = loanCount.NumberOfLoans;
                    }
                }

                returnObject.Add(obj);
            }

            returnObject = returnObject.Where(item => item["NumberOfLoans"] != null)
                .OrderByDescending(item => item["NumberOfLoans"])
                .Take(5).ToList();


            return Json(returnObject);
        }
    }
}
