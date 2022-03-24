using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person.ViewModels
{
    public class PersonLanguagesViewModel
    {
        public string PersonName { get; set; }
        public int PersonId { get; set; }
        public List<Language.Language> Languages { get; set; }

        public PersonLanguagesViewModel()
        {
            Languages = new List<Language.Language>();
        }
    }
}
