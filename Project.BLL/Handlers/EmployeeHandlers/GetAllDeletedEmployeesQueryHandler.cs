using MediatR;
using Project.BLL.Caching;

namespace Project.BLL.Handlers.EmployeeHandlers
{
    public class GetAllDeletedEmployeesQueryHandler : IRequestHandler<GetAllDeletedEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _cache;

        public GetAllDeletedEmployeesQueryHandler(IUnitOfWork unitOfWork, IRedisCacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllDeletedEmployeesQuery request, CancellationToken cancellationToken)
        {
            var data = _cache.GetData<IEnumerable<Employee>>("DeletedEmloyees");
            if (data is null)
                data = await _unitOfWork.Employees.GetAsync(request.filter);
            _cache.SetData("DeletedEmloyees", data);
            return data;
        }
    }
}
