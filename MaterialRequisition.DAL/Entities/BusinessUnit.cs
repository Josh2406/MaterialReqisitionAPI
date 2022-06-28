using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class BusinessUnit
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [Required, StringLength(20)]
        public string UnitCode { get; set; }

        [Required, StringLength(100)]
        public string UnitName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(250)]
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("ParentId")]
        public virtual BusinessUnit ParentUnit { get; set; }
    }
}
