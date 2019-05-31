using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Identificator { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        public string Provider { get; set; }
    }
}
