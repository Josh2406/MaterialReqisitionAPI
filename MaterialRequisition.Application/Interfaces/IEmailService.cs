using MaterialRequisition.Application.DTOs.Request;

namespace MaterialRequisition.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
