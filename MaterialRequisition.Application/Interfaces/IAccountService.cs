using MaterialRequisition.Application.DTOs;
using MaterialRequisition.Application.DTOs.Request;
using MaterialRequisition.Application.DTOs.Response;

namespace MaterialRequisition.Application.Interfaces
{
    public interface IAccountService
    {
        #region Commands
        /// <summary>
        /// Create a new staff account
        /// </summary>
        /// <param name="request">Account Payload</param>
        /// <returns>Return <see cref="GeneralResponse"/></returns>
        Task<GeneralResponse> CreateAccountAsync(AccountPostRequest request);

        /// <summary>
        /// Update existing account
        /// </summary>
        /// <param name="accountId">Account UID</param>
        /// <param name="request">Edited Account</param>
        /// <returns><see cref="GeneralResponse"/></returns>
        Task<GeneralResponse> UpdateAccountAsync(int accountId, AccountResponse request);

        /// <summary>
        /// Delete/deactivate account
        /// </summary>
        /// <param name="accountId">Account UID</param>
        /// <returns></returns>
        Task<GeneralResponse> DeleteAccountAsync(int accountId);
        #endregion

        #region Queries
        /// <summary>
        /// Get an account by UID
        /// </summary>
        /// <param name="accountId">Account UID</param>
        /// <returns></returns>
        Task<AccountResponse> GetAccountAsync(int accountId);

        /// <summary>
        /// Get an account by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<AccountResponse> GetAccountAsync(string username);

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns></returns>
        Task<List<AccountResponse>> GetAllAccountsAsync();

        /// <summary>
        /// Get all active accounts
        /// </summary>
        /// <returns></returns>
        Task<List<AccountResponse>> GetActiveAccountsAsync();

        /// <summary>
        /// Get accounts created by an admin
        /// </summary>
        /// <param name="username">Admin Username</param>
        /// <returns></returns>
        Task<List<AccountResponse>> GetAccountsCreatedByAsync(string username);

        /// <summary>
        /// Get accounts belonging to a specific business unit
        /// </summary>
        /// <param name="unitId">Business Unit UID</param>
        /// <returns></returns>
        Task<List<AccountResponse>> GetAccountsByBusinessUnitAsync(int unitId);

        /// <summary>
        /// Get accounts by dynamic query
        /// </summary>
        /// <param name="query">Dynamic Query</param>
        /// <returns></returns>
        Task<List<AccountResponse>> GetAccountsByQueryAsync(string query);

        /// <summary>
        /// Get paged accounts
        /// </summary>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        Task<List<AccountResponse>> GetPagedAccountAsync(int pageNumber, int pageSize);
        #endregion
    }
}
