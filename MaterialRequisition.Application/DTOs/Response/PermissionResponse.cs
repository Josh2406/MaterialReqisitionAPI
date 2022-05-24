namespace MaterialRequisition.Application.DTOs.Response
{
    public class PermissionResponse
    {
        public int RoleId { get; set; }
        public int? CreatorId { get; set; }
        public string Creator { get; set; }
        public string EntityName { get; set; }
        public string EntityAccess { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
