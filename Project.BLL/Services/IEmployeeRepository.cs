namespace Project.BLL.Services
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task SoftDeleteAsync(Employee entity);
        Task ReturnAsync(Employee entity);
    }
}
