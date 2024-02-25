using GlobalImpact.Models;

namespace GlobalImpact.Data
{
    public class StationeryDb
    {
        public static List<RecItems> Items { get; }

        static StationeryDb()
        {
            Items = new List<RecItems>();
        }
    }
}

