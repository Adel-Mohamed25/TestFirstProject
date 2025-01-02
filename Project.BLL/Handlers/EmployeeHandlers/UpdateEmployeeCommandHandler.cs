using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task IRequestHandler<UpdateEmployeeCommand>.Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Employees.UpdateAsync(request.employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
