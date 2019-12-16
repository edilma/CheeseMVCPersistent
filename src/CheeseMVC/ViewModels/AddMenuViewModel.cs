using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuViewModel
    {
        [Required (ErrorMessage ="Menu Name cannot be empty")]
        [Display (Name ="Menu Name")]
        public string Name { get; set; }
        public AddMenuViewModel() 
        { }
    }
}
