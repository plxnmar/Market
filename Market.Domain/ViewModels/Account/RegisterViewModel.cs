using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен иметь длину менее 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должен иметь длину более 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(3, ErrorMessage = "Пароль должен иметь длину более 5 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
