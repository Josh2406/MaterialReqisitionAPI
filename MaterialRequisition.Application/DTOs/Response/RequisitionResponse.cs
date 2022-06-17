namespace MaterialRequisition.Application.DTOs.Response
{
    public class RequisitionResponse
    {
        public long Id { get; set; }
        public int RequesterId { get; set; }
        public int BusinessUnitId { get; set; }
        public string Summary { get; set; }
        public DateTime DateRejected { get; set; }
        public int? ApproverId { get; set; }
        public string Status { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? Rejected { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<RequisitionItemResponse> Items { get; set; }
    }
}
