using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    /// <summary>
    /// Classe de visualação do login através da API externa.
    /// </summary>
    public class ExternalLoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(18, int.MaxValue, ErrorMessage = "Idade minima de 18 anos")]
		public int Age { get; set; }

        [Display(Name = "NIF")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve conter exatamente 9 dígitos.")]
        public int? NIF { get; set; }
    }
}
