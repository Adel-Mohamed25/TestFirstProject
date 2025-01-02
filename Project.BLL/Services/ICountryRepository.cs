using System.Linq.Expressions;

namespace Project.BLL.Services
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>>? filter = null);
    }
}
