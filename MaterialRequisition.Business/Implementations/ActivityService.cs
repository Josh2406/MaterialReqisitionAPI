using AutoMapper;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.Application.Interfaces;
using MaterialRequisition.DAL.Entities;
using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaterialRequisition.Business.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IUserService _userService;
        private readonly RequisitionContext _context;
        private readonly ILogger<ActivityService> _logger;
        private readonly IMapper _mapper;

        public ActivityService(IUserService userService, RequisitionContext context, ILogger<ActivityService> logger, IMapper mapper)
        {
            _userService = userService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task LogActivity(long recordId, string command, string recordName, string entitySchema)
        {
            try
            {
                var user = await _userService.GetSignedInUserAsync();
                if(user != null)
                {
                    var activity = new ActivityTimeline
                    {
                        AccountId = user.AccountId,
                        Command = command,
                        DateCreated = DateTime.Now,
                        RecordId = recordId.ToString(),
                        RecordSchemaName = entitySchema
                    };

                    await _context.ActivityTimelines.AddAsync(activity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LogActivity() error");
            }
        }

        public async Task<List<ActivityTimelineResponse>> GetActivitiesAsync(string username = null)
        {
            var result = new List<ActivityTimelineResponse>();
            try
            {
                UserResponse user = null;
                if(username == null)
                {
                    user = await _userService.GetSignedInUserAsync();
                }
                else
                {
                    user = await _userService.GetUserAsync(username);
                }

                if(user != null)
                {
                    var activities = await _context.ActivityTimelines.AsNoTracking().Where(x=>x.AccountId == user.AccountId).ToListAsync();
                    if(activities != null)
                    {
                        result = _mapper.Map<List<ActivityTimelineResponse>>(activities);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetActivitiesAsync() error");
            }
            return result; 
        }

        public async Task<List<ActivityTimelineResponse>> GetActivitiesAsync(int accountId)
        {
            var result = new List<ActivityTimelineResponse>();
            try
            {
                var activities = await _context.ActivityTimelines.AsNoTracking().Where(x => x.AccountId == accountId).ToListAsync();
                if (activities != null)
                {
                    result = _mapper.Map<List<ActivityTimelineResponse>>(activities);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetActivitiesAsync() error");
            }
            return result;
        }
    }
}
