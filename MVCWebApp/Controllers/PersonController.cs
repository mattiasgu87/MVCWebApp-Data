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

        public IActionResult ShowLanguages(int id)
        {
            Person person = _context.People.Find(id);         

            PersonLanguagesViewModel model = new PersonLanguagesViewModel();

            model.PersonName = person.Name;
            model.PersonId = person.ID;

            foreach (PersonLanguage lan in person.PersonLanguages)
            {
                model.Languages.Add(lan.Language);
            }                      

            return View(model);
        }

        public IActionResult AddLanguage()
        {
            AddLanguageViewModel model = new AddLanguageViewModel();
            model.PersonIdList = new SelectList(_context.People, "ID", "Name");
            model.LanguageNameList = new SelectList(_context.Languages, "LanguageName", "LanguageName");

            return View(model);
        }

        [HttpPost]
        public IActionResult AddPersonLanguage(AddLanguageViewModel AddViewModel)
        {          
            Person person = _context.People.Find(AddViewModel.PersonId);

            bool knowsLanguage = false;
            foreach (PersonLanguage pl in person.PersonLanguages)
            {
                if (pl.LanguageName == AddViewModel.LanguageName)
                { 
                    knowsLanguage = true;
                    break;
                }
            }         

            if(knowsLanguage == false)
            {
                Language language = _context.Languages.Find(AddViewModel.LanguageName);

                PersonLanguage personlanguage = new PersonLanguage();
                personlanguage.PersonId = AddViewModel.PersonId;
                personlanguage.LanguageName = AddViewModel.LanguageName;

                person.PersonLanguages.Add(personlanguage);
                language.PersonLanguages.Add(personlanguage);

                _context.PersonLanguages.Add(personlanguage);

                _context.People.Update(person);
                _context.Languages.Update(language);

                _context.SaveChanges();

                PersonLanguage test = _context.PersonLanguages.First();

                return RedirectToAction("Index");
            }
            else
            {
                //todo - improve in Identity assigment
                MessageViewModel messageModel = new MessageViewModel();
                messageModel.Message = "Person already knows that language!";

                return View("MessageView", messageModel);
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
