using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.City
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public virtual Country.Country Country { get; set; }

        public virtual List<Person.Person> People { get; set; }

        public City()
        {
            People = new List<Person.Person>();
        }
    }
}
