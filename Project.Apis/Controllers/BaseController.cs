using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Helper;
using System.Net;

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator mediatorService;

        protected IMediator mediator => mediatorService ??= HttpContext.RequestServices.GetService<IMediator>();

        private IMapper mapperService;

        protected IMapper mapper => mapperService ??= HttpContext.RequestServices.GetService<IMapper>();


        public ObjectResult Result<T>(Response<T> response)
        {
            switch (response.Code)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
    }
}
