using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalImpact.ViewModels.NewFolder
{
    /// <summary>
    /// Classe de visualição da página de criar ecoponto.
    /// </summary>
    public class CreateRecyclingBinViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Type")]
        public string[] Type { get; set; }
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "CurrentCapacity")]
        public int CurrentCapacity { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? RBTList { get; set; }
    }
}
