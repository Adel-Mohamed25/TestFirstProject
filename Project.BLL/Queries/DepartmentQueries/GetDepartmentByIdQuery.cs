using MediatR;

namespace Project.BLL.Queries.DepartmentQueries
{
    public record GetDepartmentByIdQuery(int Id) : IRequest<Department>;
}
