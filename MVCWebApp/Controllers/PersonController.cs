using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApp.Data;
using MVCWebApp.Models.Person;
using MVCWebApp.Models.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context, IPersonRepository personRepository)
        {
            _context = context;
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            CombinedPersonViewModel model = new CombinedPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities.OrderBy(c => c.CityName), "ID", "CityName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {

                _personRepository.Add(CreateViewModel);

                return RedirectToAction(nameof(Index));
            }

            CombinedPersonViewModel model = new CombinedPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities, "ID", "CityName");

            return View(nameof(Index), model);
        }

        public IActionResult Delete(int id)
        {
            _personRepository.Delete(id);

            CombinedPersonViewModel model = new CombinedPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult Search(SearchPersonViewModel searchOptions)
        {

            CombinedPersonViewModel model = new CombinedPersonViewModel();
            model.PersonList = _personRepository.Search(searchOptions.SearchTerm, searchOptions.CaseSensitive);
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult SortByCity(SortOptionsViewModel sortOptions)
        {
            CombinedPersonViewModel model = new CombinedPersonViewModel();

            model.PersonList = _personRepository.Sort(sortOptions, "city");
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult SortByName(SortOptionsViewModel sortOptions)
        {
            CombinedPersonViewModel model = new CombinedPersonViewModel();

            model.PersonList = model.PersonList = _personRepository.Sort(sortOptions, "name");
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }
    }
}
