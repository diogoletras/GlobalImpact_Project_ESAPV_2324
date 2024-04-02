using GlobalImpact.Models;

namespace GlobalImpact.ViewModels.RecyclingBin
{
    /// <summary>
    /// Classe ViewModel de simulação do ato de reciclagem.
    /// </summary>
    public class ReciclarViewModel
    {
        public Models.RecyclingBin EcoPonto { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public List<RecItems> RecItems { get; set; }
        public List<RecItems> Reciclados { get; set; }
    }
}
