using AutoMapper;
using MaterialRequisition.Application.Constants;
using MaterialRequisition.Application.DTOs;
using MaterialRequisition.Application.DTOs.Request;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.Application.Interfaces;
using MaterialRequisition.Business.Extensions;
using MaterialRequisition.DAL.Entities;
using MaterialRequisition.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MaterialRequisition.Business.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly RequisitionContext _context;
        private readonly ILogger<AccountService> _logger;
        private readonly IUserService _userService;
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public AccountService(RequisitionContext context, ILogger<AccountService> logger, IUserService userService, IActivityService activityService,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _activityService = activityService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateAccountAsync(AccountPostRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            _logger.LogInformation("Account Payload >>> {json}", json);
            GeneralResponse result = new();
            try
            {
                var currentUserTask = _userService.GetSignedInUserAsync();
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower()
                || x.Phone == request.Phone);
                if(account != null)
                {
                    result = new GeneralResponse
                    {
                        Data = new object(),
                        ResponseCode = ResponseCodes.RECORD_EXISTS,
                        ResponseMessage = "An account with similar details exists!"
                    };
                }
                else
                {
                    var businessUnit = await _context.BusinessUnits.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.BusinessUnitId);
                    if(businessUnit == null)
                    {
                        result = new GeneralResponse
                        {
                            Data = new object(),
                            ResponseCode = ResponseCodes.NOT_FOUND,
                            ResponseMessage = $"Business unit not found for BU({request.BusinessUnitId})"
                        };
                    }
                    else
                    {
                        var currentUser = await currentUserTask;

                        account = new Account
                        {
                            BusinessUnitId = request.BusinessUnitId,
                            DateCreated = DateTime.Now,
                            Email = request.Email,
                            FirstName = request.FirstName,
                            Gender = request.Gender,
                            IsActive = true,
                            LastName = request.LastName,
                            ManagerId = 0,
                            MiddleName = request.MiddleName,
                            Phone = request.Phone,
                            StaffId = request.StaffId
                        };

                        await _context.Accounts.AddAsync(account);
                        await _context.SaveChangesAsync();

                        await _activityService.LogActivity(account.Id, ActivityCommand.CREATE, account.FullName(), EntitySchemaNames.ACCOUNTS);

                        result = new GeneralResponse
                        {
                            Data = _mapper.Map<AccountResponse>(account),
                            ResponseCode = ResponseCodes.SUCCESS,
                            ResponseMessage = "OK"
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAccountAsync() error");
                result = new GeneralResponse
                {
                    Data = new object(),
                    ResponseCode = ResponseCodes.SERVER_ERROR,
                    ResponseMessage = "Error encountered while processing request."
                };
            }
            return result;
        }

        public async Task<GeneralResponse> UpdateAccountAsync(int accountId, AccountResponse request)
        {
            GeneralResponse result = new();
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
                if(account != null)
                {
                   account = _mapper.Map<Account>(request);
                   _context.Entry(account).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    await _activityService.LogActivity(accountId, ActivityCommand.UPDATE, account.FullName(), EntitySchemaNames.ACCOUNTS);

                    result = new GeneralResponse
                    {
                        Data = _mapper.Map<AccountResponse>(account),
                        ResponseCode = ResponseCodes.SUCCESS,
                        ResponseMessage = "OK"
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAccountAsync() error");
                result = new GeneralResponse
                {
                    Data = new object(),
                    ResponseCode = ResponseCodes.SERVER_ERROR,
                    ResponseMessage = "Error encountered while processing request."
                };
            }

            return result;
        }

        public async Task<GeneralResponse> DeleteAccountAsync(int accountId)
        {
            GeneralResponse result = new();
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
                if(account == null)
                {
                    result = new GeneralResponse { ResponseCode = ResponseCodes.NOT_FOUND, ResponseMessage = "Account to delete NOT FOUND!" };
                }
                else
                {
                    account.IsActive = false;
                    _context.Entry(account).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    result = new GeneralResponse { ResponseCode = ResponseCodes.SUCCESS, ResponseMessage = "Success" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAccountAsync({accountId}) error", accountId);
                result = new GeneralResponse
                {
                    Data = new object(),
                    ResponseCode = ResponseCodes.SERVER_ERROR,
                    ResponseMessage = "Error encountered while processing request."
                };
            }

            return result;
        }

        public async Task<AccountResponse> GetAccountAsync(int accountId)
        {
            AccountResponse accountResponse = new();
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
                if(account != null)
                {
                    accountResponse = _mapper.Map<AccountResponse>(account);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAccountAsync({accountId}) error", accountId);
            }
            return accountResponse;
        }

        public async Task<AccountResponse> GetAccountAsync(string username)
        {
            AccountResponse accountResponse = new();
            try
            {
                var user = await _userService.GetUserAsync(username);
                if(user != null)
                {
                    accountResponse = await GetAccountAsync(user.AccountId);
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAccountAsync({username}) error", username);
            }
            return accountResponse;
        }

        public async Task<List<AccountResponse>> GetAccountsByBusinessUnitAsync(int unitId)
        {
            var result = new List<AccountResponse>();
            try
            {
                var accounts = await _context.Accounts.AsNoTracking().Where(x=>x.BusinessUnitId == unitId).ToListAsync();
                if(accounts != null && accounts.Any())
                {
                    result = _mapper.Map<List<AccountResponse>>(accounts);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAccountsByBusinessUnitAsync({unitId}) error", unitId);
            }
            return result;
        }

        public async Task<List<AccountResponse>> GetAccountsByQueryAsync(string query)
        {
            var result = new List<AccountResponse>();
            try
            {
                var accounts = await _context.Accounts.AsNoTracking().Where(x =>
                        x.StaffId == query ||
                        x.LastName.ToLower() == query.ToLower() ||
                        x.FirstName.ToLower() == query.ToLower() ||
                        x.Email.ToLower() == query.ToLower() ||
                        x.MiddleName.ToLower() == query.ToLower() ||
                        x.BusinessUnitId.ToString() == query)
                    .ToListAsync();

                if(accounts != null && accounts.Any())
                {
                    result = _mapper.Map<List<AccountResponse>>(accounts);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAccountsByQueryAsync({query}) error", query);
            }
            return result;
        }

        public async Task<List<AccountResponse>> GetAccountsCreatedByAsync(string username)
        {
            var result = new List<AccountResponse>();
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
                if(user != null)
                {
                    var accIds = _context.ActivityTimelines.AsNoTracking()
                        .Where(x => x.AccountId == user.AccountId && x.RecordSchemaName == EntitySchemaNames.ACCOUNTS
                        && x.Command == ActivityCommand.CREATE)
                        .OrderBy(x => x.Id)
                        .Select(x => x.RecordId).ToList();

                    foreach(var acc in accIds)
                    {
                        var id = int.Parse(acc);
                        var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                        var accountResponse = _mapper.Map<AccountResponse>(account);
                        result.Add(accountResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAccountsCreatedByAsync({username}) error", username);
            }
            return result;
        }

        public async Task<List<AccountResponse>> GetActiveAccountsAsync()
        {
            var result = new List<AccountResponse>();
            try
            {
                var accounts = await _context.Accounts.AsNoTracking().Where(x => x.IsActive).ToListAsync();
                if(accounts != null && accounts.Any())
                {
                    result = _mapper.Map<List<AccountResponse>>(accounts);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetActiveAccountsAsync() error");
            }
            return result;
        }

        public async Task<List<AccountResponse>> GetAllAccountsAsync()
        {
            var result = new List<AccountResponse>();
            try
            {
                var accounts = await _context.Accounts.AsNoTracking().ToListAsync();
                if (accounts != null && accounts.Any())
                {
                    result = _mapper.Map<List<AccountResponse>>(accounts);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAccountsAsync() error");
            }
            return result;
        }

        public async Task<List<AccountResponse>> GetPagedAccountAsync(int pageNumber, int pageSize)
        {
            var result = new List<AccountResponse>();
            try
            {
                var toSkip = (pageNumber - 1) * pageSize;
                var accounts = await _context.Accounts.AsNoTracking().OrderBy(x=>x.Id).Skip(toSkip).Take(pageSize).ToListAsync();
                if(accounts != null && accounts.Any())
                {
                    result = _mapper.Map<List<AccountResponse>>(accounts);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPagedAccountAsync({pageNumber}, {pageSize}) error", pageNumber, pageSize);
            }
            return result;
        }
    }
}
