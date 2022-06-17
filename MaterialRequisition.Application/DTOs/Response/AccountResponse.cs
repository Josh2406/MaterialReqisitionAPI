namespace MaterialRequisition.Application.DTOs.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public int? CreatorId { get; set; }
        public int? ManagerId { get; set; }
        public string Manager { get; set; }
        public string CreatedByUser { get; set; }
        public int BusinessUnitId { get; set; } 
        public string BusinessUnit { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
