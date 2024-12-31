using MediatR;

namespace Project.BLL.Commands.DepartmentCommands
{
    public record CreateDepartmentCommand(Department Department) : IRequest;
}
