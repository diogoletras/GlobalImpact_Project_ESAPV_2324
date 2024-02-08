using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalImpact.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
