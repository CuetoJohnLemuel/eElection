namespace eElection.Models
{
    public class ElectionResultViewModel
    {
        public Election Election { get; set; }
        public List<Position> Positions { get; set; }
        public Dictionary<int, List<string>> Candidates { get; set; }
    }

}
