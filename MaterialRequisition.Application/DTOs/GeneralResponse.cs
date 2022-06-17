using MaterialRequisition.Application.Constants;

namespace MaterialRequisition.Application.DTOs
{
    public class GeneralResponse
    {
        public GeneralResponse()
        {
            ResponseCode = ResponseCodes.BAD_REQUEST;
            ResponseMessage = "Bad Request";
            Data = new object();
        }

        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}
