namespace eElection.Models
{
    public class VoteRequest
    {
        public int VoterId { get; set; }
        public int ElectionId { get; set; }
        public int PositionId { get; set; }
        public int CandidateId { get; set; }







        public int PresidentId { get; set; }
        public int VicePresidentId { get; set; }
        public int DistrictRepId { get; set; }
        public int PartyListRepId { get; set; }
        public List<int> Senators { get; set; }
        public List<int> MidSenators { get; set; }
        public List<int> MidSanggunianPanlalawigan { get; set; }
        public List<int> MidSanggunianBayan { get; set; }
        public List<int> MidSanggunianBarangay { get; set; }

        public int MidDistrictRepId { get; set; }
        public int MidPartyListRepId { get; set; }
        public int MidRegionalGovernorId { get; set; }
        public int MidRegionalViceGovernorId { get; set; }
        public int MidProvincialGovernorId { get; set; }
        public int MidProvincialViceGovernorId { get; set; }
        public int MidMayorId { get; set; }
        public int MidViceMayorId { get; set; }
        public int MidBarangayCaptainId { get; set; }

        public VoteRequest()
        {
            Senators = new List<int>();
            MidSenators = new List<int>();
            MidSanggunianPanlalawigan = new List<int>();

        }

    }
}
