using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class ReturnEmployeeCommandHandler : IRequestHandler<ReturnEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReturnEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReturnEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Employees.ReturnAsync(request.employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
