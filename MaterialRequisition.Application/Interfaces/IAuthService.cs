using MaterialRequisition.Application.DTOs.Request;
using MaterialRequisition.Application.DTOs.Response;

namespace MaterialRequisition.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
