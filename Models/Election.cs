using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class Election
    {
        [Key]
        public int ElectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string ElectionName { get; set; }

        [Required]
        public string ElectionTypes { get; set; } // Store selected types as comma-separated values (CSV)

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; } // Active, Inactive
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Candidate>? Candidates { get; set; }
        //public virtual ICollection<ElectionTypePositions>? ElectionTypePositions { get; set; }
    }
}
