using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? CreatorById { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required, StringLength(250)]
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("CreatorById")]
        public virtual User CreatedByUser { get; set; }
    }
}
