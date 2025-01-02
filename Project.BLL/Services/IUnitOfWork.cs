namespace Project.BLL.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        ICityRepository Cities { get; }
        ICountryRepository Countries { get; }
        IDistrictRepository Districts { get; }

        Task<int> SaveChangesAsync();
    }
}
