using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding
            modelBuilder.Entity<Person>().HasData(new Person { ID = 1, Name = "Sten Stensson", City = "Stenstorp", PhoneNumber = "0743345431" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 2, Name = "Anna Aronsson", City = "Arboga", PhoneNumber = "0743345412" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 3, Name = "Jens Falk", City = "Stockholm", PhoneNumber = "0743345444" });
        }
    }
}
