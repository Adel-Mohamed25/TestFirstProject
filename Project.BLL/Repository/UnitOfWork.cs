using Microsoft.Extensions.Logging;

namespace Project.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        //public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<Employee> Employees { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            Departments = new DepartmentRepository(_context, _logger);
            Employees = new GenericRepository<Employee>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes in UnitOfWork.");
                throw;
            }
        }
    }
}
