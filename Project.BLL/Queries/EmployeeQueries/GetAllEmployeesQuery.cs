using MediatR;

namespace Project.BLL.Queries.EmployeeQueries
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<Employee>>;

}
