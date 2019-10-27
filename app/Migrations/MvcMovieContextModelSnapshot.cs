﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

namespace asp_mvc.Migrations
{
    [DbContext(typeof(MvcMovieContext))]
    partial class MvcMovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MvcMovie.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Rating")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genre = "Romantic Comedy",
                            Price = 7.99m,
                            Rating = "R",
                            ReleaseDate = new DateTime(1989, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "QmLIoDCR4+XdfbABuDKlxGnRf8rgJSxuFKlaxXV5p+c="
                        },
                        new
                        {
                            Id = 2,
                            Genre = "Comedy",
                            Price = 8.99m,
                            ReleaseDate = new DateTime(1984, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "QqcNP7BNJ4p4G056h4ZXGw=="
                        },
                        new
                        {
                            Id = 3,
                            Genre = "Comedy",
                            Price = 9.99m,
                            ReleaseDate = new DateTime(1986, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "8dCMc07jZ4lGIgsOTsCnRA=="
                        },
                        new
                        {
                            Id = 4,
                            Genre = "Western",
                            Price = 3.99m,
                            ReleaseDate = new DateTime(1959, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "TUfObcZkBTNczp+QeDa8Zw=="
                        });
                });

            modelBuilder.Entity("MvcMovie.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Article")
                        .HasColumnType("longtext");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieID");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Article = "ZMhZSRV/DXqjBTa6ekvAAICpqePIZcD+JqpSuqCpVUI=",
                            MovieID = 1
                        });
                });

            modelBuilder.Entity("MvcMovie.Models.Review", b =>
                {
                    b.HasOne("MvcMovie.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
