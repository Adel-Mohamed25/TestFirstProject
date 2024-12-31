using MediatR;

namespace Project.BLL.Commands.DepartmentCommands
{
    public record DeleteDepartmentCommand(Department Department) : IRequest;
}
