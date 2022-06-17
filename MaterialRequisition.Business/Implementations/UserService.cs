using AutoMapper;
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
        private readonly ICachingService _cachingService;
        private readonly IMapper _mapper;

        public UserService(IJWTManager jwtManager, ILogger<UserService> logger, RequisitionContext context, ICachingService cachingService,
            IMapper mapper)
        {
            _jwtManager = jwtManager;
            _logger = logger;
            _context = context;
            _cachingService = cachingService;
            _mapper = mapper;
        }

        public async Task<UserResponse> GetSignedInUserAsync()
        {
            UserResponse result = new();
            try
            {
                var username = _jwtManager.GetCurrentlyLoggedInUsername();
                if (!string.IsNullOrEmpty(username))
                {
                    //Try retrieving current user from cache
                    result = _cachingService.RetrieveCacheEntry<UserResponse>(username);
                    if(result == null)
                    {
                        //User not found in cache, retrieve from DB and set DB output in cache
                        result = await GetUserAsync(username);
                        if(result != null) _cachingService.SetCacheEntry(username, result, 20);
                    }
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
                    result = _mapper.Map<UserResponse>(user);
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
                result = _mapper.Map<UserResponse>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync(int userId)");
            }
            return result;
        }
    }
}
