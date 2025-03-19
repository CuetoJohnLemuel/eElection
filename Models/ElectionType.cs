using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class ElectionType
    {
        [Key]
        public int ElectionTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string ElectionTypeName { get; set; }

        public virtual ICollection<ElectionTypePositions> ElectionTypePositions { get; set; } = new List<ElectionTypePositions>();
    }
}