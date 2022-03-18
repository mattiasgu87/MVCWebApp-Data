using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Language.ViewModels
{
    public class CombinedLanguageViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }
        public CreateLanguageViewModel CreateViewModel { get; set; }
    }
}
