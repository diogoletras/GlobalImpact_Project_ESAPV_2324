using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.Models
{
    public class RecyclingBinType
    {
        [Key]
        public Guid RecyclingBinTypeId { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
