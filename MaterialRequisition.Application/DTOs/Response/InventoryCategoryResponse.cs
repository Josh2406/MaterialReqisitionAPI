namespace MaterialRequisition.Application.DTOs.Response
{
    public class InventoryCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
