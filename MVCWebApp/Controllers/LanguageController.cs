using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Data;
using MVCWebApp.Models.Language;
using MVCWebApp.Models.Language.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class LanguageController : Controller
    {
        public readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CombinedLanguageViewModel model = new CombinedLanguageViewModel();
            model.LanguageList = _context.Languages.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateLanguageViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Languages.Find(CreateViewModel.LanguageName) == null)
                {
                    Language language = new Language();
                    language.LanguageName = CreateViewModel.LanguageName;

                    _context.Languages.Add(language);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //error message-> change to partial view?
                    return Content("Language already exists");
                }
            }

            CombinedLanguageViewModel model = new CombinedLanguageViewModel();
            model.LanguageList = _context.Languages.ToList();

            return View(nameof(Index), model);
        }
    }
}
