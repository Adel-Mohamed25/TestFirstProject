using MediatR;
using Project.BLL.Commands.EmployeeCommands;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IServicesRepo<Employee> employee;
        private readonly IMapper mapper;

        public CreateEmployeeCommandHandler(IServicesRepo<Employee> employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }

        public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var data = mapper.Map<Employee>(request.Employee);
            await employee.CreateAsync(data);
        }
    }
}
