using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int ElectionId { get; set; }
        public int PositionId { get; set; }
        public int CandidateId { get; set; }
        public int VoterId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
