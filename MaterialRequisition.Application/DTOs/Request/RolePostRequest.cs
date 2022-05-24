using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class RolePostRequest
    {
        [Required, StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
    }
}
