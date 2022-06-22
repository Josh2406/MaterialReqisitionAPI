﻿using AutoMapper;
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

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPagedAccountAsync({pageNumber}, {pageSize}) error", pageNumber, pageSize);
            }
            return result;
        }

        
    }
}
