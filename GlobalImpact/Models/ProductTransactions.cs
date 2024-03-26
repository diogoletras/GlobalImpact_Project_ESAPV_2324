using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
    public class ProductTransactions
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TotalPoints { get; set; }
    }
}
