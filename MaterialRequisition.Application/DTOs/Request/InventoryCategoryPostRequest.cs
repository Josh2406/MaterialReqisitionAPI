using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class InventoryCategoryPostRequest
    {
        [Required, StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
    }
}
