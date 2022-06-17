namespace MaterialRequisition.Application.DTOs.Response
{
    /// <summary>
    /// Response DTO for Role entity <br/>
    /// [<b>Configured for automapping</b>]
    /// </summary>
    public class RoleResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
