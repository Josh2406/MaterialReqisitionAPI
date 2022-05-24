using System.ComponentModel.DataAnnotations;

namespace MaterialRequisition.Application.DTOs.Request
{
    public class StockPostRequest
    {
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }

        [Required, StringLength(50)]
        public string Measurement { get; set; }
    }
}
