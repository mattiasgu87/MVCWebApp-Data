using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person.ViewModels
{
    public class AddLanguageViewModel
    {
        public int PersonId { get; set; }
        public string LanguageName { get; set; }
        public SelectList PersonIdList { get; set; }
        public SelectList LanguageNameList { get; set; }
    }
}
