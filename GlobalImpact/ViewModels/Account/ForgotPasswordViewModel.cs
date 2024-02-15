using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    /// <summary>
    /// Classe de visualização da página "Forgot PassWord".
    /// </summary>
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
