using MediatR;
using Project.BLL.Queries.EmployeeQueries;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IServicesRepo<Employee> employee;

        public GetEmployeeByIdQueryHandler(IServicesRepo<Employee> employee)
        {
            this.employee = employee;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await employee.GetByIdAsync(emp => emp.Employee_Id == request.Id);
        }
    }
}
