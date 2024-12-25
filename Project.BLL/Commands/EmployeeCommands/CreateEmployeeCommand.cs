using MediatR;
using Project.BLL.DTO;

namespace Project.BLL.Commands.EmployeeCommands
{
    public record CreateEmployeeCommand(EmployeeDTO Employee) : IRequest;

}
