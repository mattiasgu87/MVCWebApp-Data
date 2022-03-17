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
            modelBuilder.Entity<City>()
                .Property<string>("CountryForeignKey");

            modelBuilder.Entity<Person>()
                .Property<int>("CityForeignKey");

            // configures one-to-many relationship City-Country
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
            .HasForeignKey("CountryForeignKey");

            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.People)
            .HasForeignKey("CityForeignKey");

            //seeding

            //Countries
            //Country sverige = new Country();
            //sverige.CountryName = "Sverige";
            //sverige.Cities = new List<City>();
            //List<City> testcities = new List<City>();
            //testcities.Add(new City { ID = 1, CityName

            //modelBuilder.Entity<Country>().HasData(new Country { CountryName = "Norge", Cities = new List<City>(});
            //modelBuilder.Entity<Country>().HasData(new Country { CountryName = "Damnark" });

            //cities
            //City stockholm = new City();
            //stockholm.ID = 1;
            //stockholm.CityName = "Stockholm";
            //stockholm.Country = sverige;
            //stockholm.People = new List<Person>();           

            //modelBuilder.Entity<City>().HasData(new City { ID = 1, CityName = "Göteborg", Country = sverige});
            //modelBuilder.Entity<City>().HasData(new City { ID = 2, CityName = "Stockholm" });
            //modelBuilder.Entity<City>().HasData(new City { ID = 3, CityName = "Malmö" });

            //people
            //Person per = new Person();
            //per.ID = 1;
            //per.Name = "Per Persson";
            ////per.City = stockholm;
            //per.PhoneNumber = "2131233212";

            //modelBuilder.Entity<Person>().HasData(new Person { ID = 1, Name = "Sten Stensson", City = "Stenstorp", PhoneNumber = "0743345431" });
            //modelBuilder.Entity<Person>().HasData(new Person { ID = 2, Name = "Anna Aronsson", City = "Arboga", PhoneNumber = "0743345412" });
            //modelBuilder.Entity<Person>().HasData(new Person { ID = 3, Name = "Jens Falk", City = "Stockholm", PhoneNumber = "0743345444" });

            //Add people relations
            //stockholm.People.Add(per);

            ////add city relations
            //sverige.Cities.Add(stockholm);



            //modelBuilder.Entity<Country>().HasData(sverige);
            //modelBuilder.Entity<City>().HasData(stockholm);
            //modelBuilder.Entity<Person>().HasData(per);


            #region seeding
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryName = "Sverige" },
                new Country { CountryName = "Norge" },
                new Country { CountryName = "Danmark" });

            modelBuilder.Entity<City>().HasData(
            new { ID = 1, CityName = "Stockholm", CountryForeignKey = "Sverige" },
            new { ID = 2, CityName = "Oslo", CountryForeignKey = "Norge" },
            new { ID = 3, CityName = "Köpenhamn", CountryForeignKey = "Danmark" });

            modelBuilder.Entity<Person>().HasData(
                new { ID = 1, Name = "Jens Jensson", PhoneNumber = "123456789", CityForeignKey = 1 },
                new { ID = 2, Name = "Anna Andersson", PhoneNumber = "987654321", CityForeignKey = 2 },
                new { ID = 3, Name = "Sven Svensson", PhoneNumber = "123123123", CityForeignKey = 3 });
            #endregion
        }
    }
}
