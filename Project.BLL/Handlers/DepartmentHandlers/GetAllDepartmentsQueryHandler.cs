using MediatR;
using Project.BLL.Caching;
using Project.BLL.Queries.DepartmentQueries;

namespace Project.BLL.Handlers.DepartmentHandlers
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<Department>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _cache;

        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork, IRedisCacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var data = _cache.GetData<IEnumerable<Department>>("Departments");
            if (data is not null)
            {
                return data;
            }
            data = await _unitOfWork.Departments.GetAsync();
            _cache.SetData("Departments", data);
            return data;
        }
    }
}
