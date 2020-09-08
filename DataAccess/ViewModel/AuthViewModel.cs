using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModel
{
    public class AuthViewModel
    {
        //[Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string LogInEmail { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string LogInPassword { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        //[Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string SignUpEmail { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string SignUpPassword { get; set; }

        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string SignUpConfirmPassword { get; set; }
    }
}
