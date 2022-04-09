using System;
using System.ComponentModel.DataAnnotations;

namespace AsturianuTV.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указан Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Name")]
        public DateTime Years { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
