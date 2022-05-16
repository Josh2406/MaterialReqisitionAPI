using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class ItemStock
    {
        [Key]
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }

        [Required, StringLength(50)]
        public string Measurement { get; set; }

        [Required, StringLength(200)]
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("InventoryItemId")]
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
