using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eElection.Models
{
    public class Voter
    {
        [Key]
        public int VoterId { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public DateOnly? Birthdate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(255)]
        public string ProfilePhoto { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Approval Process Fields
        [Required]
        public string Status { get; set; } = "Pending";

        public string? RejectionReason { get; set; }

        // // Relationships
        // public int? AccountId { get; set; } // 🔗 Explicit Foreign Key
        // [ForeignKey("AccountId")]
        public Account? Account { get; set; }

        public virtual ICollection<Candidate>? Candidates { get; set; }
    }
}
