using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
    public class ProductTransactionStatus
    {
        [Key]
        public Guid ProductTransactionStatusId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
