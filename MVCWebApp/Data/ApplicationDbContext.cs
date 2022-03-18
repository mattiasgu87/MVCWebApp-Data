using Microsoft.EntityFrameworkCore;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Foreign key shadow properties
            modelBuilder.Entity<City>()
                .Property<string>("CountryForeignKey");
            modelBuilder.Entity<Person>()
                .Property<int>("CityForeignKey");

            //Configures one-to-many relationship City-Country
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
            .HasForeignKey("CountryForeignKey");

            //Configures one-to-many relationship Person-City
            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.People)
            .HasForeignKey("CityForeignKey");     

            #region seeding
            //seeding countries
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryName = "Sverige" },
                new Country { CountryName = "Norge" },
                new Country { CountryName = "Danmark" });

            //seeding cities
            modelBuilder.Entity<City>().HasData(
            new { ID = 1, CityName = "Stockholm", CountryForeignKey = "Sverige" },
            new { ID = 2, CityName = "Oslo", CountryForeignKey = "Norge" },
            new { ID = 3, CityName = "Köpenhamn", CountryForeignKey = "Danmark" });

            //seeding people
            modelBuilder.Entity<Person>().HasData(
                new { ID = 1, Name = "Jens Jensson", PhoneNumber = "123456789", CityForeignKey = 1 },
                new { ID = 2, Name = "Anna Andersson", PhoneNumber = "987654321", CityForeignKey = 2 },
                new { ID = 3, Name = "Sven Svensson", PhoneNumber = "123123123", CityForeignKey = 3 });
            #endregion
        }
    }
}
