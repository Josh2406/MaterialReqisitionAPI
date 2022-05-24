using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialRequisition.DAL.Entities
{
    public class StockPosting
    {
        public long Id { get; set; }
        public int PosterId { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }

        [Required, StringLength(50)]
        public string Measurement { get; set; }

        [Required, StringLength(250)]
        public string Hash { get; set; }

        public DateTime DatePosted { get; set; }

        [ForeignKey("PosterId")]
        public virtual Account Poster { get; set; }

        [ForeignKey("InventoryItemId")]
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
