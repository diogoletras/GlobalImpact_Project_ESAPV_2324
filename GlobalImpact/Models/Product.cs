using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


		[ForeignKey("ProductCategoryId")]
		public string ProductCategoryId { get; set; }
		[NotMapped]
		public virtual ProductCategory Category { get; set; }
	}
}
