namespace MaterialRequisition.Application.DTOs.Response
{
    public class ActivityTimelineResponse
    {
        public long Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string RecordSchemaName { get; set; }
        public string RecordId { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
