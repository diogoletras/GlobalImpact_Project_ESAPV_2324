using Microsoft.Build.Framework;

namespace GlobalImpact.ViewModels.RecyclingBin
{
    /// <summary>
    /// Classe ViewModel de simulação de introdução do id do ecoponto.
    /// </summary>
    public class EcoLogViewModel
    {
        [Required]
        public string? IdInput { get; set; }
    }
}
