namespace MaterialRequisition.Application.DTOs.Response
{
    /// <summary>
    /// Response DTO for User entity <br/>
    /// [<b>Configured for automapping</b>]
    /// </summary>
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int AccountId { get; set; }
    }
}
