using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Data;
using MVCWebApp.Models;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class CountryController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CombinedCountryViewModel model = new CombinedCountryViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {
                if(_context.Countries.Find(CreateViewModel.CountryName) == null)
                {
                    Country country = new Country();
                    country.CountryName = CreateViewModel.CountryName;

                    _context.Countries.Add(country);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //todo - improve in Identity assigment
                    MessageViewModel messageModel = new MessageViewModel();
                    messageModel.Message = "Country already exists!";

                    return View("MessageView", messageModel);
                }
            }

            CombinedCountryViewModel model = new CombinedCountryViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(nameof(Index), model);
        }

        public IActionResult Delete(string id)
        {
            Country countryToDelete = _context.Countries.Find(id);

            if(countryToDelete != null)
            {
                foreach (City city in countryToDelete.Cities)
                {
                    foreach (Person person in city.People)
                    {
                        _context.People.Remove(person);
                    }

                    _context.Cities.Remove(city);
                }

                _context.Countries.Remove(countryToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
