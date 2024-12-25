using MediatR;

namespace Project.BLL.Queries.EmployeeQueries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<Employee>;
}
