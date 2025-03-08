using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required]
        [StringLength(200)]
        public string PartyName { get; set; }

        [Required]
        [StringLength(200)]
        public string Leader { get; set; }

        [Required]
        public int FoundedYear { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Candidate>? Candidates { get; set; }
    }
}

