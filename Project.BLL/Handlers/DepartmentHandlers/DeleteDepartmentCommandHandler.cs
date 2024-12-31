using MediatR;
using Project.BLL.Commands.DepartmentCommands;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Departments.DeleteAsync(request.Department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
