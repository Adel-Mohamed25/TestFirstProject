using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class SoftDeleteEmployeeCommandHandler : IRequestHandler<SoftDeleteEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task IRequestHandler<SoftDeleteEmployeeCommand>.Handle(SoftDeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Employees.SoftDeleteAsync(request.employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
