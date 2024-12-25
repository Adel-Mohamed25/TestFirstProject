using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IServicesRepo<Employee> employee;

        public DeleteEmployeeCommandHandler(IServicesRepo<Employee> employee)
        {
            this.employee = employee;
        }
        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await employee.DeleteAsync(request.employee);
        }
    }
}
