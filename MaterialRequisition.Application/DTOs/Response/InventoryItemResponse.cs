namespace MaterialRequisition.Application.DTOs.Response
{
    public class InventoryItemResponse
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int? CreatorId { get; set; }
        public string CreatedBy { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string StandardUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
