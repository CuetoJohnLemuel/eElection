namespace eElection.Models
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int PartyId { get; set; }
        public string Biography { get; set; }
    }
}
