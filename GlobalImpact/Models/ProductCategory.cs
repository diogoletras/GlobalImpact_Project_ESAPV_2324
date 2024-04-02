using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
	public class ProductCategory
	{
		[Key]
		public Guid ProductCategoryId { get; set; }
		[Required]
		public string Category { get; set; }
	}
}
