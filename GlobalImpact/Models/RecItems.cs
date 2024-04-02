namespace GlobalImpact.Models
{
    /// <summary>
    /// Classe modelo RecItems.
    /// Dados sobre os items reciclaveis.
    /// </summary>
    public class RecItems
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public double Peso { get; set; }
        public int Pontos { get; set; }
    }
}
