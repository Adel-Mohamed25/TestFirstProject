using MediatR;

namespace Project.BLL.Queries.DepartmentQueries
{
    public record GetAllDepartmentsQuery() : IRequest<IEnumerable<Department>>;
}
