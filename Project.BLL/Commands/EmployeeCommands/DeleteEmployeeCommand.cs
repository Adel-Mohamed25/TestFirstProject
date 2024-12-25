using MediatR;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record DeleteEmployeeCommand(Employee employee) : IRequest;
}
