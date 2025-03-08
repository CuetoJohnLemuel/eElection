using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateId { get; set; }

        [Required]
        public int ElectionId { get; set; }

        [Required]
        public int VoterId { get; set; }

        [Required]
        public int PartyId { get; set; }

        [Required]
        public int PositionId { get; set; }

        public string? Biography { get; set; }

        // Navigation Properties (Foreign Keys)
        [ForeignKey("ElectionId")]
        public virtual Election? Election { get; set; }

        [ForeignKey("VoterId")]
        public virtual Voter? Voter { get; set; }

        [ForeignKey("PartyId")]
        public virtual Party? Party { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position? Position { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
