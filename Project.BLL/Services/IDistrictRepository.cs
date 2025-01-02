using System.Linq.Expressions;

namespace Project.BLL.Services
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>>? filter = null);
    }
}
