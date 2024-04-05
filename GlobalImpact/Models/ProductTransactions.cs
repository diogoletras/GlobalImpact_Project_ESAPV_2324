using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
    public class ProductTransactions
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid TransactionId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        [ForeignKey("TransactionStatusId")]
        public Guid TransactionStatusId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int Quantity { get; set; }

        [NotMapped]
        public AppUser user { get; set; }
        [NotMapped]
        public Product product { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string TransStatus { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}
