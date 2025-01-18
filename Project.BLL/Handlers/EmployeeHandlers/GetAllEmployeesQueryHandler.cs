using MediatR;
using Project.BLL.Caching;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _cache;

        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IRedisCacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var data = _cache.GetData<IEnumerable<Employee>>("Employees");
            if (data is null)
                data = await _unitOfWork.Employees.GetAsync(request.filter);
            _cache.SetData("Employees", data);
            return data;
        }
    }
}
