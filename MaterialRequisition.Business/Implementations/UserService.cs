using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.Application.Interfaces;
using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaterialRequisition.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly IJWTManager _jwtManager;
        private readonly ILogger<UserService> _logger;
        private readonly RequisitionContext _context;

        public UserService(IJWTManager jwtManager, ILogger<UserService> logger, RequisitionContext context)
        {
            _jwtManager = jwtManager;
            _logger = logger;
            _context = context;
        }

        public async Task<UserResponse> GetSignedInUserAsync()
        {
            UserResponse result = new();
            try
            {
                var username = _jwtManager.GetCurrentlyLoggedInUsername();
                if (!string.IsNullOrEmpty(username))
                {
                    result = await GetUserAsync(username);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSignedInUserAsync()");
            }
            return result;
        }

        public async Task<UserResponse> GetUserAsync(string username)
        {
            UserResponse result = new();
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Username.ToLower() == username.ToLower());
                    if (user != null)
                    {
                        result = new UserResponse { Username = user.Username, AccountId = user.AccountId, Id = user.Id};
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync(string username)");
            }
            return result;
        }

        public async Task<UserResponse> GetUserAsync(int userId)
        {
            UserResponse result = new();
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
                if (user != null)
                {
                    result = new UserResponse { Username = user.Username, AccountId = user.AccountId, Id = user.Id };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync(int userId)");
            }
            return result;
        }
    }
}
