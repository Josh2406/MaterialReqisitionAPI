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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialRequisition.Business.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly RequisitionContext _context;
        private readonly ILogger<AccountService> _logger;
        private readonly IUserService _userService;

        public AccountService(RequisitionContext context, ILogger<AccountService> logger, IUserService userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
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
                        account = new Account
                        {
                            BusinessUnitId = request.BusinessUnitId,
                            CreatorId = currentUser.AccountId,
                            DateCreated = DateTime.Now,
                            Email = request.Email,
                            FirstName = request.FirstName,
                            Gender = request.Gender,
                            IsActive = true,
                            LastName = request.LastName,
                            ManagerId = request.ManagerId,
                            MiddleName = request.MiddleName,
                            Phone = request.Phone,
                            StaffId = request.StaffId
                        };
                        await _context.Accounts.AddAsync(account);
                        await _context.SaveChangesAsync();

                        result = new GeneralResponse
                        {
                            Data = account
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAccountAsync() error");
            }
            return result;
        }

        public async Task<GeneralResponse> DeleteAccountAsync(int accountId)
        {
            throw new NotImplementedException();
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

        public async Task<GeneralResponse> UpdateAccountAsync(int accountId, AccountPostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
