namespace MaterialRequisition.Application.DTOs.Response
{
    /// <summary>
    /// Response DTO for StockResponse entity <br/>
    /// [<b>Configured for automapping</b>]
    /// </summary>
    public class StockResponse
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }
        public string HashStamp { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
