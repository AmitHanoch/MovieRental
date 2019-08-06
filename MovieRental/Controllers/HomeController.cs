
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;
using Newtonsoft.Json.Linq;
using System;
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
            ViewBag.GenresAndAverageAges = this.GetGenresAndAverageAges();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetMostPopularForList()
        {
            List<JObject> returnObject = new List<JObject>();

            // select top 5 m.Name, loans.NumberOfLoans from Movie m,
            // (select l.MovieId, COUNT(l.MovieId) as NumberOfLoans from Loan l group by l.MovieId) loans
            // where m.MovieId = loans.MovieId
            // order by loans.NumberOfLoans DESC
            var query = from m in _context.Movie
                        join l in (from loans in _context.Loan
                              group loans by loans.MovieId into sub
                              select new
                              {
                                  MovieId = sub.Key,
                                  NumberOfLoans = sub.Count()
                              })
                              on m.MovieId equals l.MovieId
                        orderby l.NumberOfLoans descending
                        select new
                        {
                            Name = m.Name,
                            MovieId = m.MovieId,
                            NumberOfLoans = l.NumberOfLoans
                        };


            foreach (var item in query.Take(5))
            {
                JObject obj = new JObject();
                obj["Name"] = item.Name;
                obj["MovieId"] = item.MovieId;
                obj["NumberOfLoans"] = item.NumberOfLoans;

                returnObject.Add(obj);
            }

            return Json(returnObject);
        }

        public List<JObject> GetGenresAndAverageAges()
        {
            List<JObject> returnObject = new List<JObject>();

            // select movies.genreName, AVG(datediff(year, cust.Birthday, getdate())) as avgAge
            // from Loan loans
            // join Customer cust on cust.CustomerId = loans.CustomerId
            // join (select m.MovieId, g.Name as genreName from Movie m, Genre g where m.GenreId = g.GenreId) as movies
            //    on movies.MovieId = loans.MovieId
            // group by movies.genreName
            var query = from loans in _context.Loan
                        join cust in _context.Customer on loans.CustomerId equals cust.CustomerId
                        join movies in (from m in _context.Movie
                                        join g in _context.Genre on m.GenreId equals g.GenreId
                                        select new
                                        {
                                            MovieId = m.MovieId,
                                            GenreName = g.Name
                                        }) on loans.MovieId equals movies.MovieId
                        group new { movies, cust } by movies.GenreName into grouped
                        select new
                        {
                            GenreName = grouped.Key,
                            AverageAge = grouped.Average(x => Math.Floor((double)DateTime.Now.Subtract(x.cust.Birthday).Days / 365)),
                        };
                        

            foreach (var item in query)
            {
                JObject obj = new JObject();
                obj["GenreName"] = item.GenreName;
                obj["AverageAge"] = item.AverageAge;

                returnObject.Add(obj);
            }

            return returnObject;
        }

        public string getMovieByGenre()
        {
            var groupedBooks = _context.Movie.GroupBy(m => m.Genre);
            JObject returnObject = new JObject();

            foreach (var item in groupedBooks)
            {
                returnObject[item.Key.Name] = item.Count();
            }

            string json = returnObject.ToString();

            return json;
        }

        public string getNumberOfMovieLoans()
        {
            JArray returnObject = new JArray();

            var query = from m in _context.Movie
                         join l in _context.Loan on m.MovieId equals l.MovieId
                         group m by m.Name into groupByMovies
                         select new
                         {
                             movieName = groupByMovies.Key,
                             loansNumber = groupByMovies.Count()
                         };

            foreach (var item in query)
            {
                JObject obj = new JObject();
                obj["movieName"] = item.movieName;
                obj["loansNumber"] = item.loansNumber;
                returnObject.Add(obj);
            }

            string json = returnObject.ToString();

            return json;
        }
    }
}
