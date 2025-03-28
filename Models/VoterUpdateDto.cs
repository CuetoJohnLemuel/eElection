using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class VoterUpdateDto
    {
        public int VoterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Gender { get; set; }
        public string? GovernmentId { get; set; }
        public string? VoterPhotoId { get; set; }

    }
}

