using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$", ErrorMessage = "O Email deve conter ter o seguinte formato: exemplo@globalimpact.com")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "Idade minima de 18 anos")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve conter exatamente 9 dígitos.")]
        [Display(Name = "NIF")]
        public int? NIF { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
