namespace MaterialRequisition.Application.DTOs.Response
{
    public class StockResponse
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public int InventoryItem { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
