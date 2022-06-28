using MaterialRequisition.DAL.Entities;

namespace MaterialRequisition.Business.Extensions
{
    public static class All
    {
        public static string FullName(this Account account)
        {
            return string.IsNullOrEmpty(account.MiddleName) ? $"{account.FirstName} {account.LastName}":
                $"{account.FirstName} {account.MiddleName} {account.LastName}";
        }
    }
}
