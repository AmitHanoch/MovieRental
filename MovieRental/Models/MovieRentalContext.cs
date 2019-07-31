using Microsoft.EntityFrameworkCore;

namespace MovieRental.Models
{
    public class MovieRentalContext : DbContext
    {
        public MovieRentalContext(DbContextOptions<MovieRentalContext> options) : base(options)
        {}

        public DbSet<MovieRental.Models.Movie> Movie { get; set; }

        public DbSet<MovieRental.Models.Genre> Genre { get; set; }
        
        public DbSet<MovieRental.Models.Customer> Customer { get; set; }

        public DbSet<MovieRental.Models.Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasKey(c => new { c.MovieId, c.CustomerId, c.LoanDate });
        }

        public DbSet<MovieRental.Models.Branch> Branch { get; set; }

        public DbSet<MovieRental.Models.User> User { get; set; }
    }
}
