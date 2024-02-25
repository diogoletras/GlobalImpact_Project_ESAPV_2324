using GlobalImpact.Models;

namespace GlobalImpact.ViewModels.NewFolder
{
    public class ReciclarViewModel
    {
        public RecyclingBin EcoPonto { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public List<RecItems> RecItems { get; set; }
        public List<RecItems> Reciclados { get; set; }
    }
}
