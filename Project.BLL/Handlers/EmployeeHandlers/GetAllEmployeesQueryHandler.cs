using MediatR;
using Project.BLL.Queries.EmployeeQueries;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IServicesRepo<Employee> employee;

        public GetAllEmployeesQueryHandler(IServicesRepo<Employee> employee)
        {
            this.employee = employee;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await employee.GetAsync();
        }
    }
}
