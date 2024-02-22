using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
