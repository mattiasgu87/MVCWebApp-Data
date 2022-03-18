using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApp.Data;
using MVCWebApp.Models;
using MVCWebApp.Models.Language;
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

        public IActionResult AddLanguage()
        {
            ViewData["PersonId"] = new SelectList(_context.People, "ID", "Name");
            ViewData["LanguageName"] = new SelectList(_context.Languages, "LanguageName", "LanguageName");

            return View();
        }

        public IActionResult ShowLanguages(int id)
        {          
            List<Language> languages = new List<Language>();

            foreach (PersonLanguage lan in _context.People.Find(id).PersonLanguages)
            {
                languages.Add(lan.Language); //old
            }

            //LanguageListViewModel model = new LanguageListViewModel();
            //model.Languages = languages;


            ViewData["Person"] = new string(_context.People.Find(id).Name);
            ViewData["Languages"] = new SelectList(languages, "LanguageName", "LanguageName");

            return View();
        }

        [HttpPost]
        public IActionResult AddPersonLanguage(PersonLanguage  personLanguage)
        {
            Language language = _context.Languages.Find(personLanguage.LanguageName);
            Person person = _context.People.Find(personLanguage.PersonId);

            bool exists = false;
            foreach (PersonLanguage pl in person.PersonLanguages)
            {
                if (pl.LanguageName == personLanguage.LanguageName)
                { 
                    exists = true;
                    break;
                }
            }         

            if(exists == false)
            {
                person.PersonLanguages.Add(personLanguage);
                language.PersonLanguages.Add(personLanguage);

                _context.PersonLanguages.Add(personLanguage);

                _context.People.Update(person);
                _context.Languages.Update(language);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                //error message-> change to partial view? Viewbag?
                return Content("Person already knows that language");
            }

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
