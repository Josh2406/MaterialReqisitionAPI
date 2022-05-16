using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.DAL.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
