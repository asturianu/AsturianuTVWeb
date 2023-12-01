using System;
using System.ComponentModel.DataAnnotations;

namespace AsturianuTV.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Surname is required field")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Name is required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
