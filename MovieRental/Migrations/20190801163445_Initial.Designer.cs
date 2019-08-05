﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieRental.Models;

namespace MovieRental.Migrations
{
    [DbContext(typeof(MovieRentalContext))]
<<<<<<< HEAD:MovieRental/Migrations/20190801163445_Initial.Designer.cs
    [Migration("20190801163445_Initial")]
=======
    [Migration("20190731170807_Initial")]
>>>>>>> f56624d60ae78ed0418283662100d6732993b470:MovieRental/Migrations/20190731170807_Initial.Designer.cs
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieRental.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<double>("LocationX");

                    b.Property<double>("LocationY");

                    b.HasKey("BranchId");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("MovieRental.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FamilyName")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("PersonalId")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("MovieRental.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MovieRental.Models.Loan", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LoanDate");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("MovieId", "CustomerId", "LoanDate");

                    b.HasAlternateKey("CustomerId", "LoanDate", "MovieId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("MovieRental.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenreId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("Producer")
                        .IsRequired();

                    b.Property<DateTime>("ReleaseDate");

                    b.HasKey("MovieId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieRental.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MovieRental.Models.Loan", b =>
                {
                    b.HasOne("MovieRental.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieRental.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MovieRental.Models.Movie", b =>
                {
                    b.HasOne("MovieRental.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
