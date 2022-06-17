using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class AccountPostRequest 
    {
        [Required, StringLength(20)]
        public string StaffId { get; set; }
        public int BusinessUnitId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(6)]
        public string Gender { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(14)]
        public string Phone { get; set; }

    }
}
