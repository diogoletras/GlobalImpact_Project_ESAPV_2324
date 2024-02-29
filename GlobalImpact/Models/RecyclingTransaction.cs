using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace GlobalImpact.Models
{
	/// <summary>
	/// Classe de modelo das transações.
	/// </summary>
	public class RecyclingTransaction
	{
		[Key]
		public Guid Id { get; set; }
		[ForeignKey("UserId")]
		public virtual AppUser User { get; set; }
		[ForeignKey("RecyclingBinId")]
		public virtual RecyclingBin RecyclingBin { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
		public DateTime Date { get; set; }
        [Required]
        public int Points { get; set; }

    }
}
