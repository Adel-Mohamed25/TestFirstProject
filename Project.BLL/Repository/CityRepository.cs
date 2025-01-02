using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CityRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>>? filter = null)
        {
            if (filter != null)
                return await _context.Cities.AsNoTracking().Where(filter).Include(dis => dis.Country).ToListAsync();
            return await _context.Cities.AsNoTracking().Include(dis => dis.Country).ToListAsync();
        }
    }
}
