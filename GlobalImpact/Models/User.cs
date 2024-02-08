using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GlobalImpact.Models
{
    public class User : IdentityUser
    {
        [Required]
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        [Required]
        [ProtectedPersonalData]
        public string LastName { get; set; }

        [Required]
        [ProtectedPersonalData]
        public int Age { get; set; }

        [PersonalData]
        public int Points { get; set; } = 0;

        [ProtectedPersonalData]
        public int? NIF { get; set; }
    }
}
