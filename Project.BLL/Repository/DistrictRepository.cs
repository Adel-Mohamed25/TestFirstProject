using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DistrictRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>>? filter = null)
        {
            if (filter != null)
                return await _context.Districts.AsNoTracking().Where(filter).Include(dis => dis.City).ToListAsync();
            return await _context.Districts.AsNoTracking().Include(dis => dis.City).ToListAsync();
        }
    }
}
