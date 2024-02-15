using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    /// <summary>
    /// Class de visualização da página de confirmação de email.
    /// </summary>
    public class ConfirmEmailViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
