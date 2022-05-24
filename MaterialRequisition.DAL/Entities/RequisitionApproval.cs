using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class RequisitionApproval
    {
        [Key]
        public long Id { get; set; }
        public long RequsitionId { get; set; }
        public string Status { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("RequsitionId")]
        public virtual Requisition Requisition { get; set; }
    }
}
