using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class RequisitionApprovalPostRequest
    {
        public long RequsitionId { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; }

        [StringLength (150)]
        public string Description { get; set; }
    }
}
