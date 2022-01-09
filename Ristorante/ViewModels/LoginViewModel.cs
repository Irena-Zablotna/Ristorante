using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.VievModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage ="campo obbligatorio")]
        public string Username   { get; set; }


        [Required(ErrorMessage = "campo obbligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
      
    }
}
