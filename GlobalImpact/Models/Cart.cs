using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalImpact.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
		[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
		[Required]
        public int Quantity { get; set; }
    }
}
