namespace eElection.Models
{
    public class VoteRequest
    {
        public int VoterId { get; set; }
        // National election fields
        public int PresidentId { get; set; }
        public int VicePresidentId { get; set; }
        public int DistrictRepId { get; set; }
        public int PartyListRepId { get; set; }
        public List<int> Senators { get; set; }

        // Midterm election fields
        public List<int> MidSenators { get; set; }

        // Constructor to initialize lists
        public VoteRequest()
        {
            Senators = new List<int>();
            MidSenators = new List<int>();
        }

    }
}
