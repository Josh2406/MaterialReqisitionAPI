using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? CreatorId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string EntityName { get; set; }

        [Required, StringLength(20)]
        public string EntityAccess { get; set; }
        public bool IsActive { get; set; }

        [Required, StringLength(250)]
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("CreatorId")]
        public virtual Account CreatorAccount { get; set; }
    }
}
