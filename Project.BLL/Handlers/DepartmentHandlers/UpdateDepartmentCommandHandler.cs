using Project.BLL.Commands.DepartmentCommands;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Departments.UpdateAsync(request.Department);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
