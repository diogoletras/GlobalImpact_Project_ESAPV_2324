using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
    }
}
