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
                            Birthday = new DateTime(1998, 2, 15),
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
                            Birthday = new DateTime(1990, 5, 22),
                            Email = "lola2@gmail.com",
                        },
                        new Customer
                        {
                            //CustomerId = 3,
                            PersonalId = "232423446",
                            FirstName = "Sami",
                            FamilyName = "Hacabay",
                            Gender = "male",
                            PhoneNumber = "0503356133",
                            Address = "Ramat Hasharon",
                            Birthday = new DateTime(1997, 3, 29),
                            Email = "lola3@gmail.com",
                        },
                        new Customer
                        {
                            //CustomerId = 4,
                            PersonalId = "223423456",
                            FirstName = "lily",
                            FamilyName = "Bloom",
                            Gender = "female",
                            PhoneNumber = "0526642133",
                            Address = "Rehovot",
                            Birthday = new DateTime(1998, 2, 15),
                            Email = "lola4@gmail.com",
                        },
                        new Customer
                        {
                            //CustomerId = 5,
                            PersonalId = "222223446",
                            FirstName = "Tal",
                            FamilyName = "Hanoch",
                            Gender = "male",
                            PhoneNumber = "0509932133",
                            Address = "Ashdod",
                            Birthday = new DateTime(1980, 12, 8),
                            Email = "lola5@gmail.com",
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
                            ReleaseDate = new DateTime(2000, 5, 5),
                            GenreId = 1,
                            Producer = "David Heyman",
                            Price = 3.90m,
                        },
                        new Movie
                        {
                            Name = "Shrek",
                            ReleaseDate = new DateTime(2005, 3, 15),
                            GenreId = 3,
                            Producer = "John H. Williams",
                            Price = 3.90m,
                        },
                        new Movie
                        {
                            Name = "Lion King",
                            ReleaseDate = new DateTime(2019, 1, 1),
                            GenreId = 1,
                            Producer = "Greg Daniels",
                            Price = 13.90m,
                        },
                        new Movie
                        {
                            Name = "Spiderman",
                            ReleaseDate = new DateTime(2013, 4, 29),
                            GenreId = 5,
                            Producer = "Greg Daniels",
                            Price = 13.90m,
                        },
                        new Movie
                        {
                            Name = "Avengers",
                            ReleaseDate = new DateTime(2012, 7, 29),
                            GenreId = 3,
                            Producer = "Lauren Faust",
                            Price = 5.90m,
                        },
                        new Movie
                        {
                            Name = "Venom",
                            ReleaseDate = new DateTime(2020, 4, 2),
                            GenreId = 2,
                            Producer = "John H. Williams",
                            Price = 4.90m,
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
                            LoanDate = new DateTime(2010, 4, 2),
                            ReturnDate = new DateTime(2010, 5, 1),
                        },
                        new Loan
                        {
                            CustomerId = 2,
                            MovieId = 2,
                            LoanDate = new DateTime(2010, 3, 2),
                            ReturnDate = new DateTime(2010, 4, 1),
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
