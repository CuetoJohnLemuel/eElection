using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public int VoterId { get; set; } // Foreign key to the voter
        public int? PresidentId { get; set; } // Nullable for positions that may not be voted on
        public int? VicePresidentId { get; set; }
        public int? DistrictRepId { get; set; }
        public int? PartyListRepId { get; set; }
        public string? Senators { get; set; } // Comma-separated list of candidate IDs

        public string? MidSenators { get; set; } // Comma-separated list of candidate IDs


        //public int? RegionalGovernorId { get; set; }
        //public int? RegionalViceGovernorId { get; set; }
        //public string RegionalAssemblyMembers { get; set; } // Comma-separated list
        //public int? ProvincialGovernorId { get; set; }
        //public int? ProvincialViceGovernorId { get; set; }
        //public string SangguniangPanlalawiganMembers { get; set; } // Comma-separated list
        //public int? MayorId { get; set; }
        //public int? ViceMayorId { get; set; }
        //public string SangguniangPanlungsodBayanMembers { get; set; } // Comma-separated list
        //public int? BarangayCaptainId { get; set; }
        //public string SangguniangBarangayMembers { get; set; } // Comma-separated list
        //public int? SKChairmanId { get; set; }
        //public string SKMembers { get; set; } // Comma-separated list
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
