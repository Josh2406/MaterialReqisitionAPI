using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class RequisitionItem
    {
        [Key]
        public long Id { get; set; }
        public int InventoryItemId { get; set; }
        public long RequisitionId { get; set; }

        [Required, StringLength(50)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("InventoryItemId")]
        public virtual InventoryItem InventoryItem { get; set; }

        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
    }
}
