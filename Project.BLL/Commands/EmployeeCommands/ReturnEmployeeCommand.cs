using MediatR;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record ReturnEmployeeCommand(Employee employee) : IRequest;
}
