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

                // Look for any produchers.
                if (!context.Producer.Any())
                {
                    context.Producer.AddRange(
                        new Producer
                        {
                            Name = "David Heyman"
                        },
                        new Producer
                        {
                            Name = "Greg Daniels"
                        },
                        new Producer
                        {
                            Name = "John H. Williams"
                        },
                        new Producer
                        {
                            Name = "Lauren Faust"
                        }
                    );
                }

                // Look for any Branchs.
                if (!context.Branch.Any())
                {
                    context.Branch.AddRange(
                        new Branch
                        {
                            Address = "Raanana",
                            LocationX = 32.18489140558821,
                            LocationY = 34.87343421596455
                        },
                        new Branch
                        {
                            Address = "Rehovot",
                            LocationX = 31.897669361644866,
                            LocationY = 34.813842901551524
                        },
                        new Branch
                        {
                            Address = "Ganei Tikva",
                            LocationX = 32.06244122293621,
                            LocationY = 34.87091187713511
                        }
                    );
                }

                // Look for any users.
                if (!context.User.Any())
                {
                    context.User.AddRange(
                        new User
                        {
                            Username = "admin",
                            Password = "1234",
                            RoleId = 1,
                        },
                        new User
                        {
                            Username = "shopKeeer",
                            Password = "1234",
                            RoleId = 2
                        }
                    );
                }

                // Look for any users.
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Name = "Harry Poter",
                            ReleaseDate = new DateTime(11111111110),
                            GenreId = 1,
                            ProducerId = 2,
                            Price = 3.90m,
                        },
                        new Movie
                        {
                            Name = "Shrek",
                            ReleaseDate = new DateTime(22222222),
                            GenreId = 3,
                            ProducerId = 1,
                            Price = 3.90m,
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
