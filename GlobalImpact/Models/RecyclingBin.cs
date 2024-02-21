using System.ComponentModel.DataAnnotations;
using GlobalImpact.Data;
using GlobalImpact.Enumerates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace GlobalImpact.Models
{
    public class RecyclingBin
    {
        [PersonalData]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public RecyclingBinType Type { get; set; }

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int CurrentCapacity { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
