using MediatR;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record SoftDeleteEmployeeCommand(Employee employee) : IRequest;
}
