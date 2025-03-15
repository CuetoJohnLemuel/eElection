namespace eElection.Models
{
    public class VoteRequest
    {
        public int VoterId { get; set; }
        public int? PresidentId { get; set; }
        public int? VicePresidentId { get; set; }
        public int? DistrictRepId { get; set; }
        public int? PartyListRepId { get; set; }
        public List<int> Senators { get; set; }
    }
}
