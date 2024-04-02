using GlobalImpact.Models;

namespace GlobalImpact.Utils
{
    public sealed class CartSingleton
    {
        private CartSingleton() { }
        public static List<Product> instance = null;
        public static List<Product> Instance { 
            get { if(instance == null) { instance = new List<Product>(); } return instance; }
        }
    }
}
