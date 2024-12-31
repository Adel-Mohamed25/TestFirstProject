using MediatR;
using Project.BLL.Commands.DepartmentCommands;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Departments.CreateAsync(request.Department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
