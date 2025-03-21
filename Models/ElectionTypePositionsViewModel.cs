namespace eElection.Models
{
    public class ElectionTypePositionsViewModel
    {
        public int ElectionTypeId { get; set; }
        public string ElectionTypeName { get; set; }
        public string Positions { get; set; } // Concatenated Positions
    }
}
