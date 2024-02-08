using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
