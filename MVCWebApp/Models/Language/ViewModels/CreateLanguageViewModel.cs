using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Language.ViewModels
{
    public class CreateLanguageViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Language Name")]
        public string LanguageName { get; set; }

        public CreateLanguageViewModel()
        {

        }
    }
}
