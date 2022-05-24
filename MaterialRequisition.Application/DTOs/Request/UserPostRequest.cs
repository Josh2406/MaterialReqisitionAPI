using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class UserPostRequest
    {
        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string PasswordCompare { get; set; }

        public int AccountId { get; set; }
    }
}
