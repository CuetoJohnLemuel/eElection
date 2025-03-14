namespace eElection.Models
{
    public class VoteRequest
    {
        public int? President { get; set; }
        public int? VicePresident { get; set; }
        public List<int>? Senators { get; set; }
        public int? DistrictRep { get; set; }
        public int? PartyListRep { get; set; }
    }
}
