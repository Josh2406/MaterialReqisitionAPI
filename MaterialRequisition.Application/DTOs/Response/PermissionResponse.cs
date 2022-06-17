namespace MaterialRequisition.Application.DTOs.Response
{
    public class FullPermissionResponse: PermissionResponse
    {
        public int? CreatedById { get; set; }
        public int RoleId { get; set; }
        public string CreatedByUser { get; set; }
        public string EntityName { get; set; }
        public string EntityAccess { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class PermissionResponse
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
