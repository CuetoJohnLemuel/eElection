using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eElection.Models
{
    public class ElectionTypePositions
    {
        [Key]
        public int ElectionTypePositionId { get; set; }

        [Required]
        public int ElectionTypeId { get; set; }

        [Required]
        public int PositionId { get; set; }

        [ForeignKey("ElectionTypeId")]
        public virtual ElectionType ElectionType { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
    }
}
