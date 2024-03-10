using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.RecyclingBin
{
    /// <summary>
    /// Classe ViewModel da filtragem de ecopontos.
    /// </summary>
    public class FilterViewModel
    {
        public IEnumerable<GlobalImpact.Models.RecyclingBin> RecyclingBins { get; set; }
        public Models.RecyclingBin RecyclingBin { get; set; }
        public double? Capacity { get; set; }
        public double? CurrentCapacity { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }

    }
}
