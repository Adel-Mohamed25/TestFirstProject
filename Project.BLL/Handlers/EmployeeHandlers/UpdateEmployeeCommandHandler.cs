using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IServicesRepo<Employee> employee;

        public UpdateEmployeeCommandHandler(IServicesRepo<Employee> employee)
        {
            this.employee = employee;
        }

        async Task IRequestHandler<UpdateEmployeeCommand>.Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await employee.UpdateAsync(request.employee);
        }
    }
}
