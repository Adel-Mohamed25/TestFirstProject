using MediatR;
using System.Linq.Expressions;

namespace Project.BLL.Queries.EmployeeQueries
{
    public record GetAllEmployeesQuery(Expression<Func<Employee, bool>>? filter = default) : IRequest<IEnumerable<Employee>>;

}
