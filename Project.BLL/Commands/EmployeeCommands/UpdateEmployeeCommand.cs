using MediatR;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record UpdateEmployeeCommand(Employee employee) : IRequest;

}
