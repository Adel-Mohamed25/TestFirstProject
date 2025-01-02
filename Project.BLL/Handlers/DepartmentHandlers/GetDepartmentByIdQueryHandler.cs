using MediatR;
using Project.BLL.Queries.DepartmentQueries;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Department>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Department> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Departments.GetByIdAsync(dep => dep.Department_Id == request.Id);
        }
    }
}
