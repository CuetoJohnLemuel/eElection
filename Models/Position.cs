using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(200)]
        public string PositionName { get; set; }

        [Required]
        public int MaxCandidates { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Candidate>? Candidates { get; set; }
        public virtual ICollection<ElectionTypePositions> ElectionTypePositions { get; set; } = new List<ElectionTypePositions>();
    }
}
