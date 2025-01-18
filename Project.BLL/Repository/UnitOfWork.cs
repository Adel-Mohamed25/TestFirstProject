using Microsoft.Extensions.Logging;

namespace Project.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }
        public ICityRepository Cities { get; }
        public ICountryRepository Countries { get; }
        public IDistrictRepository Districts { get; }

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            Departments = new DepartmentRepository(_context, _logger);
            Employees = new EmployeeRepository(_context, _logger);
            Cities = new CityRepository(_context, _logger);
            Districts = new DistrictRepository(_context, _logger);
            Countries = new CountryRepository(_context, _logger);
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
                _logger.LogCritical(ex, "Error occurred while saving changes in UnitOfWork.");
                throw;
            }
        }
    }
}
