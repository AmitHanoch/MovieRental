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
                            //GenreId = 1,
                            Name = "Kids"
                        },
                        new Genre
                        {
                            //GenreId = 2,
                            Name = "Fantasy"
                        },
                        new Genre
                        {
                            //GenreId = 3,
                            Name = "Drama"
                        },
                        new Genre
                        {
                            //GenreId = 4,
                            Name = "Comics"
                        },
                        new Genre
                        {
                            //GenreId = 5,
                            Name = "Cooking"
                        }
                    );
                }

                // Look for any produchers.
                if (!context.Producer.Any())
                {
                    context.Producer.AddRange(
                        new Producer
                        {
                            //ProducerId = 1,
                            Name = "David Heyman"
                        },
                        new Producer
                        {
                            //ProducerId = 2,
                            Name = "Greg Daniels"
                        },
                        new Producer
                        {
                            //ProducerId = 3,
                            Name = "John H. Williams"
                        },
                        new Producer
                        {
                            //ProducerId = 4,
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
                            //BranchId = 1,
                            Address = "Raanana",
                            LocationX = 32.18489140558821,
                            LocationY = 34.87343421596455
                        },
                        new Branch
                        {
                            //BranchId = 2,
                            Address = "Rehovot",
                            LocationX = 31.897669361644866,
                            LocationY = 34.813842901551524
                        },
                        new Branch
                        {
                            //BranchId = 3,
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

                // Look for any customers.
                if (!context.Customer.Any())
                {
                    context.Customer.AddRange(
                        new Customer
                        {
                            //CustomerId = 1,
                            PersonalId = "222223445",
                            FirstName = "mama",
                            FamilyName = "Reala",
                            Gender = "female",
                            PhoneNumber = "0509932132",
                            Address = "La La Land",
                            Birthday = new DateTime(),
                            Email = "lola@gmail.com",

                        },
                        new Customer
                        {
                            //CustomerId = 2,
                            PersonalId = "222223446",
                            FirstName = "papa",
                            FamilyName = "Reala",
                            Gender = "male",
                            PhoneNumber = "0509932133",
                            Address = "La La Land",
                            Birthday = new DateTime(),
                            Email = "lola2@gmail.com",
                        }
                    );
                }

                context.SaveChanges();

                // Look for any movies.
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Name = "Harry Poter",
                            ReleaseDate = new DateTime(),
                            GenreId = 1,
                            ProducerId = 2,
                            Price = 3.90m,
                            TrailerLink = "https://www.youtube.com/watch?v=eKSB0gXl9dw",
                        },
                        new Movie
                        {
                            Name = "Shrek",
                            ReleaseDate = new DateTime(),
                            GenreId = 3,
                            ProducerId = 1,
                            Price = 3.90m,
                            TrailerLink = "https://www.youtube.com/watch?v=W37DlG1i61s",
                        }
                    );
                }

                context.SaveChanges();

                // Look for any loans.
                if (!context.Loan.Any())
                {
                    context.Loan.AddRange(
                        new Loan
                        {
                            CustomerId = 1,
                            MovieId = 1,
                            LoanDate = new DateTime(),
                            ReturnDate = new DateTime(),
                        },
                        new Loan
                        {
                            CustomerId = 2,
                            MovieId = 2,
                            LoanDate = new DateTime(),
                            ReturnDate = new DateTime(),
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
