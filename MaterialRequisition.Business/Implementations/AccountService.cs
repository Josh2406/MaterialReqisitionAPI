using AutoMapper;
using MaterialRequisition.Application.Constants;
using MaterialRequisition.Application.DTOs;
using MaterialRequisition.Application.DTOs.Request;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.Application.Interfaces;
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
        private readonly IMapper _mapper;

        public AccountService(RequisitionContext context, ILogger<AccountService> logger, IUserService userService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateAccountAsync(AccountPostRequest request)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(request));
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
                        var unitManager = 
                            await _context.BusinessUnitManagers.AsNoTracking().FirstOrDefaultAsync(x => x.BusinessUnitId == businessUnit.Id);

                        account = new Account
                        {
                            BusinessUnitId = request.BusinessUnitId,
                            CreatedByUserId = currentUser.AccountId,
                            CreatedByUsername = currentUser.Username,
                            DateCreated = DateTime.Now,
                            Email = request.Email,
                            FirstName = request.FirstName,
                            Gender = request.Gender,
                            IsActive = true,
                            LastName = request.LastName,
                            ManagerId = unitManager?.Id,
                            MiddleName = request.MiddleName,
                            Phone = request.Phone,
                            StaffId = request.StaffId
                        };

                        await _context.Accounts.AddAsync(account);
                        await _context.SaveChangesAsync();

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

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAccountAsync() error");
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
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetAccountAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetAccountsByBusinessUnitAsync(int unitId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetAccountsByQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetAccountsCreatedByAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetActiveAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetAllAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountResponse>> GetPagedAccountAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        
    }
}
