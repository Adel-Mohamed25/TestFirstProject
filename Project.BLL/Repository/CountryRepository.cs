using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CountryRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>>? filter = null)
        {
            if (filter != null)
                return await _context.Countries.AsNoTracking().Where(filter).ToListAsync();
            return await _context.Countries.AsNoTracking().ToListAsync();
        }
    }
}
