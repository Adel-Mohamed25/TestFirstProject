namespace Project.BLL.Services
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Department> Departments { get; }

        public IDepartmentRepository Departments { get; }

        IGenericRepository<Employee> Employees { get; }

        Task<int> SaveChangesAsync();
    }
}
