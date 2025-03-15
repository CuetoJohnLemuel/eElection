using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eElection.Models
{
    public class VoteDetail
    {
        [Key]
        public int VoteDetailId { get; set; }
        public int VoteId { get; set; }
        public int CandidateId { get; set; }

        [ForeignKey("VoteId")]
        public virtual Vote Vote { get; set; }
    }
}
