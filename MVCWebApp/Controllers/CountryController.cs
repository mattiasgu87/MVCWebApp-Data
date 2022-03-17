using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Data;
using MVCWebApp.Models.Country;
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
                Country country = new Country();
                country.CountryName = CreateViewModel.CountryName;

                _context.Countries.Add(country);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            CombinedCountryViewModel model = new CombinedCountryViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(nameof(Index), model);
        }
    }
}
