namespace MaterialRequisition.Application.DTOs.Response
{
    /// <summary>
    /// Response DTO for RequisitionItem entity <br/>
    /// [<b>Configured for automapping</b>]
    /// </summary>
    public class RequisitionItemResponse
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}
