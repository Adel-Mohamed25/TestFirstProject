using System.Linq.Expressions;

namespace Project.BLL.Services
{
    public interface IServicesRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T department);
        Task UpdateAsync(T department);
        Task DeleteAsync(T department);
    }
}
