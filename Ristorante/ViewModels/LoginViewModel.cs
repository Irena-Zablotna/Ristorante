using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.VievModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        
        [Display(Name = "Username")]
        public string Username   { get; set; }

        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


       
    }
}
