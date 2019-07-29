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
                        Id = 1,
                        Name = "Kids"
                    },
                    new Genre
                    {
                        Id = 2,
                        Name = "Fantasy"
                    },
                    new Genre
                    {
                        Id = 3,
                        Name = "Drama"
                    },
                    new Genre
                    {
                        Id = 4,
                        Name = "Comics"
                    },
                    new Genre
                    {
                        Id = 5,
                        Name = "Cooking"
                    });
                } 

                
                context.SaveChanges();
            }
        }
    }
}
