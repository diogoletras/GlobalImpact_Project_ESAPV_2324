using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace GlobalImpact.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public int Quantity { get; set; } = 1;


		[ForeignKey("ProductCategoryId")]
		public string ProductCategoryId { get; set; }
		[NotMapped]
		public ProductCategory Category { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

    }
}
