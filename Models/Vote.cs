using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int VoterId { get; set; }
        public int? PresidentId { get; set; }
        public int? VicePresidentId { get; set; }
        public int? DistrictRepId { get; set; }
        public int? PartyListRepId { get; set; }
    }
}
