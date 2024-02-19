using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    /// <summary>
    /// Classe de visualização da página "Reset PassWord".
    /// </summary>
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
