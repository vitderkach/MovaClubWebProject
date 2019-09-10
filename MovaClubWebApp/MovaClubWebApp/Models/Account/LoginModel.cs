using System.ComponentModel.DataAnnotations;

namespace MovaClubWebApp.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Identificator { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
