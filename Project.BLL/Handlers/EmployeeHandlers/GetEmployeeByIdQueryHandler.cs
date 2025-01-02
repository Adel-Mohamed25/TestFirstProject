using MediatR;
using Project.BLL.Queries.EmployeeQueries;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Employees.GetByIdAsync(emp => emp.Employee_Id == request.Id);
        }
    }
}
