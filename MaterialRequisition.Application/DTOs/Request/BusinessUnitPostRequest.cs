using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class BusinessUnitPostRequest
    {
        public int? ParentId { get; set; }

        [Required, StringLength(20)]
        public string UnitCode { get; set; }

        [Required, StringLength(100)]
        public string UnitName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
