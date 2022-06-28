using MaterialRequisition.Application.Constants;
using MaterialRequisition.Application.DTOs.Request;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.Application.Interfaces;
using MaterialRequisition.Business.Helpers;
using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaterialRequisition.Business.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly RequisitionContext _context;
        private readonly IJwtManager _jwtManager;

        public AuthService(ILogger<AuthService> logger, RequisitionContext context, IJwtManager jwtManager)
        {
            _logger = logger;
            _context = context;
            _jwtManager = jwtManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse response = null;
            try
            {
                var user = await _context.Users.Include("Role").AsNoTracking().FirstOrDefaultAsync(x=>x.Username == request.Username);
                if (user == null)
                {
                    response = new LoginResponse
                    {
                        ResponseCode = ResponseCodes.BAD_REQUEST,
                        ResponseMessage = "Invalid username/password"
                    };
                }
                else
                {
                    var isPasswordValid = PasswordHash.ValidatePassword(request.Password, user.PasswordHash);
                    if (isPasswordValid)
                    {
                        var expiresIn = 30; //30mins
                        var token = _jwtManager.GenerateToken(user.Username, expiresIn);
                        if (!string.IsNullOrEmpty(token))
                        {
                            response = new LoginResponse
                            {
                                Data = new LoginData
                                {
                                    AuthToken = token,
                                    AuthType = "Bearer",
                                    ExpiryInMins = expiresIn,
                                    Username = request.Username
                                },
                                ResponseMessage = "OK",
                                ResponseCode = ResponseCodes.SUCCESS
                            };
                        }
                        else
                        {
                            response = new LoginResponse
                            {
                                ResponseCode = ResponseCodes.TOKEN_GENERATION_ERROR,
                                ResponseMessage = "Token generation failed. Please try again"
                            };
                        }
                    }
                    else
                    {
                        response = new LoginResponse
                        {
                            ResponseCode = ResponseCodes.BAD_REQUEST,
                            ResponseMessage = "Invalid username/password"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LoginAsync() error");
                response = new LoginResponse
                {
                    ResponseCode = ResponseCodes.SERVER_ERROR,
                    ResponseMessage = "An error occurred while generating token"
                };
            }
            return response;
        }
    }
}
