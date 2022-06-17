namespace MaterialRequisition.Application.DTOs.Response
{
    public class BusinessUnitResponse
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? CreatedById { get; set; }
        public string ParentUnit { get; set; }
        public string CreatedByUser { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
