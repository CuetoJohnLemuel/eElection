namespace eElection.Models
{
    public class CandidateExtension
    {
        public int CandidateId { get; set; }
        public string ElectionName { get; set; } = "N/A";
        public string FullName { get; set; } = "N/A";
        public string PartyName { get; set; } = "N/A";
        public string PositionName { get; set; } = "N/A";
        public string? Biography { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
