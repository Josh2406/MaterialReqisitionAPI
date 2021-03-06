using System.Linq.Expressions;

namespace MaterialRequisition.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<IEnumerable<T>> GetPagedAdvancedReponseAsync(int pageNumber, int pageSize, string fields);

        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);
    }
}
