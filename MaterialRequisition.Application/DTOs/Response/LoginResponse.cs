namespace MaterialRequisition.Application.DTOs.Response
{
    public class LoginResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public LoginData Data { get; set; }
    }

    public class LoginData
    {
        public string Username { get; set; }
        public string AuthToken { get; set; }
        public string AuthType { get; set; }
        public int ExpiryInMins { get; set; }
    }
}
