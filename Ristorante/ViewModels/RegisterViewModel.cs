using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.VievModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(12)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Le password inserite devono essere uguali ")]
        public string ConfirmPassword { get; set; }
    }
}
