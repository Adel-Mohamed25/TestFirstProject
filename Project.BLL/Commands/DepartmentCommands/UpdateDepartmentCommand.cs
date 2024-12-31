using MediatR;

namespace Project.BLL.Commands.DepartmentCommands
{
    public record UpdateDepartmentCommand(Department Department) : IRequest;
}
