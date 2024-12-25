using MediatR;
using Project.BLL.Commands.EmployeeCommands;
using Project.BLL.Repository;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class SoftDeleteEmployeeCommandHandler : IRequestHandler<SoftDeleteEmployeeCommand>
    {
        private readonly EmployeeRepo employee;

        public SoftDeleteEmployeeCommandHandler(EmployeeRepo employee)
        {
            this.employee = employee;
        }

        async Task IRequestHandler<SoftDeleteEmployeeCommand>.Handle(SoftDeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await employee.SoftDeleteAsync(request.employee);
        }
    }
}
