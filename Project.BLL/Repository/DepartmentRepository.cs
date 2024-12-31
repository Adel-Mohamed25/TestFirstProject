
using Microsoft.Extensions.Logging;

namespace Project.BLL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DepartmentRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
        }

        public async Task DeleteAsync(Department entity)
        {
            _context.Departments.Remove(entity);
        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            try
            {
                return await _context.Departments.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var result = await _context.Departments.FindAsync(id);

            if (result == null)
                throw new ArgumentNullException("No Result");
            return result;
        }

        public async Task UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
        }
    }
}
