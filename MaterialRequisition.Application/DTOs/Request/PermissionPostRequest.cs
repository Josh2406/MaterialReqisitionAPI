using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class PermissionPostRequest
    {
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string Entity { get; set; }

        [Required, StringLength(50)]
        public string EntityAccess { get; set; }
    }
}
