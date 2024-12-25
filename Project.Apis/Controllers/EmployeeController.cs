using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Commands.EmployeeCommands;
using Project.BLL.DTO;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.BLL.Queries.EmployeeQueries;
using Project.DAL.Entities;


namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public EmployeeController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }


        [HttpGet]
        [Route("~/api/Employee/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await mediator.Send(new GetAllEmployeesQuery());
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = 200,
                    Status = "Ok",
                    Message = "Succed",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = 404,
                    Status = "Not Found",
                    Message = "Not Found",
                    Data = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("~/api/Employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var data = await mediator.Send(new GetEmployeeByIdQuery(id));
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = 200,
                    Status = "Ok",
                    Message = "Succed",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = 404,
                    Status = "Not Found",
                    Message = "Not Found",
                    Data = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("~/api/Employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.CVName = employee.CV?.UploadFile("Documents");
                    employee.ImageName = employee.Image?.UploadFile("Images");
                    var data = mapper.Map<EmployeeDTO>(employee);
                    await mediator.Send(new CreateEmployeeCommand(data));
                    return Ok(new ApiResponse<EmployeeDTO>()
                    {
                        Code = 201,
                        Status = "Create",
                        Message = "Data Saved",
                        Data = data
                    });
                }
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 400,
                    Status = "Not Created",
                    Message = "Validation Erorr",
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = 400,
                    Status = "Not Created",
                    Message = "Not Created",
                    Data = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("~/api/Employee/PutEmployee")]
        public async Task<IActionResult> PutEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(employee);
                    await mediator.Send(new UpdateEmployeeCommand(data));
                    return Ok(new ApiResponse<Employee>
                    {
                        Code = 202,
                        Message = "Updated",
                        Status = "Accepted",
                        Data = data
                    });
                }
                return BadRequest(new ApiResponse<string>
                {
                    Code = 400,
                    Status = "Not Updated",
                    Message = "Validation Error",
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = 304,
                    Status = "Not Modified",
                    Message = "Not Modified",
                    Data = ex.Message
                });
            }

        }

        [HttpDelete]
        [Route("~/api/Employee/SoftDeleteEmployee")]
        public async Task<IActionResult> SoftDeleteEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                var data = mapper.Map<Employee>(employee);
                await mediator.Send(new SoftDeleteEmployeeCommand(data));
                return Ok(new ApiResponse<Employee>
                {
                    Code = 200,
                    Status = "Deleted",
                    Message = "Data Is Deleted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Code = 400,
                    Status = "Not Deleted",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("~/api/Employee/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                employee.CV?.RemoveFile("Documents");
                employee.Image?.RemoveFile("Images");
                var data = mapper.Map<Employee>(employee);
                await mediator.Send(new DeleteEmployeeCommand(data));
                return Ok(new ApiResponse<Employee>
                {
                    Code = 200,
                    Status = "Deleted",
                    Message = "Data Is Deleted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Code = 400,
                    Status = "Not Deleted",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }

    }
}
