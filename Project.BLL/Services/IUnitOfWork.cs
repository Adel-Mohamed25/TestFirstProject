namespace Project.BLL.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }

        Task<int> SaveChangesAsync();
    }
}
