using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MovieRental.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieRentalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MovieRentalContext>>()))
            {
                // Look for any movies.
                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(
                    new Genre
                    {
                        Name = "Kids"
                    },
                    new Genre
                    {
                        Name = "Fantasy"
                    },
                    new Genre
                    {
                        Name = "Drama"
                    },
                    new Genre
                    {
                        Name = "Comics"
                    },
                    new Genre
                    {
                        Name = "Cooking"
                    });
                } 

                
                context.SaveChanges();
            }
        }
    }
}
