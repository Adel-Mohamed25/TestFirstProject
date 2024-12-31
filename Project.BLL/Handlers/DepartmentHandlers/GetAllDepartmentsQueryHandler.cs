using MediatR;
using Project.BLL.Queries.DepartmentQueries;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<Department>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Departments.GetAsync();
        }
    }
}
