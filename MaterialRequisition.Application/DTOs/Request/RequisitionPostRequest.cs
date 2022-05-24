namespace MaterialRequisition.Application.DTOs.Request
{
    public class RequisitionPostRequest
    {
        public string Summary { get; set; }
        public List<RequisitionRequestItem> Items { get; set; }
    }

    public class RequisitionRequestItem
    {
        public int InventotyItemId { get; set; }
        public int Quantity { get; set; }
    }
}
