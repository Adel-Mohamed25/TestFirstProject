using MediatR;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record CreateEmployeeCommand(Employee Employee) : IRequest;

}
