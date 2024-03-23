using GlobalImpact.Models;
namespace GlobalImpact.Data
{
    public class CartItems
    {
        public static List<Product> ListItems { get; }

        static CartItems()
        {
            ListItems = new List<Product>();
        }
    }
}
