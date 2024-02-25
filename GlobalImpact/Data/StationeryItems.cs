using GlobalImpact.Models;

namespace GlobalImpact.Data
{
    public class StationeryItems
    {
        public static List<RecItems> Items { get; }

        static StationeryItems()
        {
            Items = new List<RecItems> {
                new RecItems
                {
                    Tipo = "Glass",
                    Nome = "Garrafa",
                    Peso = 0.5,
                    Pontos = 5
                },
                new RecItems
                {
                    Tipo = "Glass",
                    Nome = "Cerveja",
                    Peso = 0.2,
                    Pontos = 2
                },
                new RecItems
                {
                    Tipo = "Paper",
                    Nome = "Caixa",
                    Peso = 0.3,
                    Pontos = 3
                },
                new RecItems
                {
                    Tipo = "Paper",
                    Nome = "Cartolina",
                    Peso = 0.1,
                    Pontos = 1
                },
                new RecItems
                {
                    Tipo = "Plastic",
                    Nome = "Lata",
                    Peso = 0.2,
                    Pontos = 1
                },
                new RecItems
                {
                    Tipo = "Plastic",
                    Nome = "Taparuere",
                    Peso = 0.5,
                    Pontos = 5
                },
                new RecItems
                {
                    Tipo = "Other",
                    Nome = "Chocolate",
                    Peso = 0.2,
                    Pontos = 0
                },
                new RecItems
                {
                    Tipo = "Other",
                    Nome = "Pilhas",
                    Peso = 0.1,
                    Pontos = 0
                },
            };
        }
    }
}
