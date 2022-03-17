using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApp.Data;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class CityController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CombinedCityViewModel model = new CombinedCityViewModel();
            model.CityList = _context.Cities.OrderBy(c => c.Country).ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCityViewModel CreateCityViewModel)
        {
            if (ModelState.IsValid)
            {
                City city = new City();
                city.CityName = CreateCityViewModel.CityName;
                Country country = _context.Countries.Find(CreateCityViewModel.CountryName);
                if(country.Cities == null)
                {
                    country.Cities = new List<City>();
                }
                city.Country = country;
                country.Cities.Add(city);             

                _context.Update(country);
                _context.Add(city);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            CombinedCityViewModel model = new CombinedCityViewModel();
            model.CityList = _context.Cities.ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(nameof(Index), model);
        }

        public IActionResult Delete(int id)
        {
            City cityToDelete = _context.Cities.Find(id);

            if (cityToDelete != null)
            {
                    foreach (Person person in cityToDelete.People)
                    {
                        _context.People.Remove(person);
                    }
               
                _context.Cities.Remove(cityToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
