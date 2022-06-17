namespace MaterialRequisition.Application.DTOs.Response
{
    /// <summary>
    /// Response DTO for InventoryCategory entity <br/>
    /// [<b>Configured for automapping</b>]
    /// </summary>
    public class InventoryCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
