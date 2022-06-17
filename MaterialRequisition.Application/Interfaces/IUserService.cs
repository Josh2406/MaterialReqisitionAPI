using MaterialRequisition.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialRequisition.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserAsync(string username);
        Task<UserResponse> GetUserAsync(int userId);
        Task<UserResponse> GetSignedInUserAsync();
    }
}
