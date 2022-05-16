using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class Requisition
    {
        [Key]
        public long Id { get; set; }
        public int RequesterId { get; set; }
        public string Summary { get; set; }
        public DateTime DateRejected { get; set; }
        public int? ApproverId { get; set; }
        [Required, StringLength(50)]
        public string Status { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? Rejected { get; set; }
        public DateTime ReleaseDate { get; set; } 
        [Required, StringLength(250)] 
        public string HashStamp { get; set; }

        [ForeignKey("RequesterId")]
        public virtual Account RequesterAccount { get; set; }

        [ForeignKey("ApproverId")]
        public virtual Account ApproverAccount { get; set; }
    }
}
