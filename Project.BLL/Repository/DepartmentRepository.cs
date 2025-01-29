using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public DepartmentRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAsync(Expression<Func<Department, bool>>? filter = null)
        {
            try
            {
                if (filter == null)
                {
                    return await _context.Departments.AsNoTracking().ToListAsync();
                }
                return await _context.Departments.AsNoTracking().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Not Found Data");
                throw;
            }
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter)
        {
            try
            {
                var result = await _context.Departments.AsNoTracking().Where(filter).FirstOrDefaultAsync();

                if (result == null)
                    throw new ArgumentNullException("No Result");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Object Not Found.");
                throw;
            }
        }


        public async Task CreateAsync(Department entity, CancellationToken cancellationToken)
        {
            await _context.Departments.AddAsync(entity, cancellationToken);
        }

        public void UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
        }


        public void DeleteAsync(Department entity)
        {
            _context.Departments.Remove(entity);
        }


    }
}
