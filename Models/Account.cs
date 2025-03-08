using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eElection.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        [StringLength(150)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? EmailConfirmationToken { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Link to Voter
        public int? VoterId { get; set; } // Nullable, as some accounts may not be voters
        [ForeignKey("VoterId")]
        public Voter? Voter { get; set; }
    }
}
