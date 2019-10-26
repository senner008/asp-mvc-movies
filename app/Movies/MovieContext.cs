using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {

        public MvcMovieContext (DbContextOptions<MvcMovieContext> options, IKeys keys)
            : base(options)
        {
         
        }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
                // modelBuilder.UseEncryption(this._provider);

                base.OnModelCreating(modelBuilder);


                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    foreach (var property in entityType.GetProperties())
                    {
                        var attributes = property.PropertyInfo.GetCustomAttributes(typeof(EncryptedAttribute), false);
                        if (attributes.Any())
                        {
                            property.SetValueConverter(new EncryptedConverter());
                        }
                    }
                }

                List<Movie> Movies = new List<Movie>
                {
                    new Movie
                    {
                        Id = 1,
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = "R",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Id = 2,
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Id = 3,
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Id = 4,
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }
                };
                modelBuilder.Entity<Movie> ().HasData (Movies);
        }
    }
}