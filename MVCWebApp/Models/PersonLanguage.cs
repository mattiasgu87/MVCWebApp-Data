using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models.Person;

namespace MVCWebApp.Models
{
    public class PersonLanguage
    {
        public int PersonId { get; set; }
        public virtual Person.Person Person { get; set; }     

        public string LanguageName { get; set; }
        public virtual Language.Language Language { get; set; }
    }
}
