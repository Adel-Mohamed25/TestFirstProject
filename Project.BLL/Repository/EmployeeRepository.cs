using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Project.BLL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>>? filter = null)
        {
            try
            {
                if (filter is null)
                    return await _context.Employees.AsNoTracking()
                                                   .Include(emp => emp.Department)
                                                   .Include(emp => emp.District)
                                                   .ToListAsync();

                return await _context.Employees.AsNoTracking()
                                               .Where(filter)
                                               .Include(emp => emp.Department)
                                               .Include(emp => emp.District)
                                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Not Found Data");
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            try
            {
                var result = await _context.Employees.AsNoTracking().Where(filter)
                                                                    .Include(emp => emp.Department)
                                                                    .Include(emp => emp.District)
                                                                    .FirstOrDefaultAsync();
                if (result == null)
                {
                    throw new ArgumentException("Object Not Found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Object Not Found.");
                throw;
            }

        }

        public async Task CreateAsync(Employee entity)
        {
            entity.CreationDate = DateTime.Now;
            await _context.Employees.AddAsync(entity);
        }

        public async Task DeleteAsync(Employee entity)
        {
            _context.Employees.Remove(entity);
        }

        public async Task SoftDeleteAsync(Employee entity)
        {
            var result = await _context.Employees.FindAsync(entity.Employee_Id);
            if (result != null)
            {
                result.IsDeleted = true;
                result.IsActive = false;
                result.DeletedDate = DateTime.Now;
            }
            else
            {
                throw new ArgumentNullException("Employee Not Found");
            }
        }


        public async Task ReturnAsync(Employee entity)
        {
            var result = await _context.Employees.FindAsync(entity.Employee_Id);
            if (result != null)
            {
                result.IsDeleted = false;
                result.IsActive = true;
            }
            else
            {
                throw new ArgumentNullException("Employee Not Found");
            }
        }

        public async Task UpdateAsync(Employee entity)
        {
            entity.IsUpdated = true;
            entity.UpdatedDate = DateTime.Now;
            _context.Employees.Update(entity);
        }
    }
}
