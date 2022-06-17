using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int? CreatedById { get; set; }

        [Required, StringLength(50)]
        public string ItemName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required, StringLength(50)]
        public string StandardUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("CategoryId")]
        public virtual InventoryCategory InventoryCategory { get; set; }

        [ForeignKey("CreatedById ")]
        public virtual User CreatedBy { get; set; }
    }
}
