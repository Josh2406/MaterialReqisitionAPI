using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public int? ManagerId { get; set; }

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
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public User User { get; set; }

        [ForeignKey("BusinessUnitId")]
        public virtual BusinessUnit BusinessUnit { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Account ManagerAccount { get; set; }
    }
}
