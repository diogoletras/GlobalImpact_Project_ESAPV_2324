using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    public class ConfirmEmailViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
