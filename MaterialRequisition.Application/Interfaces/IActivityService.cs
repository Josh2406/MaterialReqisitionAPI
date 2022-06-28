namespace MaterialRequisition.Application.Interfaces
{
    public interface IActivityService
    {
        Task LogActivity(long recordId, string command, string recordName, string entitySchema);
    }
}
