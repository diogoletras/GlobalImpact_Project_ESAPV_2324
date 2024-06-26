﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GlobalImpact.Models
{
    /// <summary>
    /// Classe modelo AppUser.
    /// </summary>
    public class AppUser : IdentityUser
    {
        
        [Required]
        public String UniqueCode { get; set; }

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

        [NotMapped]
        public string? RoleId { get; set; }
        [NotMapped]
        public string? Role { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
        [NotMapped]
        // Add this roperty
        public ClaimsIdentity? Identity { get; set; }


    }
}
