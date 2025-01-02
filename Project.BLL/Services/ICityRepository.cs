using System.Linq.Expressions;

namespace Project.BLL.Services
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>>? filter = null);
    }
}
