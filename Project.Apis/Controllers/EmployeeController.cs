using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Commands.EmployeeCommands;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.BLL.Queries.EmployeeQueries;
using Project.DAL.Entities;
using System.Net;


namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Hr")]
    public class EmployeeController : BaseController
    {
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await mediator.Send(new GetAllEmployeesQuery());
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new Response<IEnumerable<EmployeeVM>>()
                {
                    Code = HttpStatusCode.OK,
                    Status = "Ok",
                    Message = "Succed",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new Response<string>()
                {
                    Code = HttpStatusCode.NotFound,
                    Status = "Not Found",
                    Message = "Not Found",
                    Data = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            try
            {
                var data = await mediator.Send(new GetEmployeeByIdQuery(id));
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new Response<EmployeeVM>()
                {
                    Code = HttpStatusCode.OK,
                    Status = "Ok",
                    Message = "Succed",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new Response<string>()
                {
                    Code = HttpStatusCode.NotFound,
                    Status = "Not Found",
                    Message = "Not Found",
                    Data = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("PostEmployee")]
        public async Task<IActionResult> PostEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.CVName = employee.CV?.UploadFile("Documents");
                    employee.ImageName = employee.Image?.UploadFile("Images");
                    var data = mapper.Map<Employee>(employee);
                    await mediator.Send(new CreateEmployeeCommand(data));
                    return Ok(new Response<Employee>()
                    {
                        Code = HttpStatusCode.Created,
                        Status = "Create",
                        Message = "Data Saved",
                        Data = data
                    });

                }
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.NotFound,
                    Status = "Not Created",
                    Message = "Validation Erorr",
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = "Not Created",
                    Message = "Not Created",
                    Data = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("PutEmployee")]
        public async Task<IActionResult> PutEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(employee);
                    await mediator.Send(new UpdateEmployeeCommand(data));
                    return Ok(new Response<Employee>
                    {
                        Code = HttpStatusCode.Accepted,
                        Message = "Updated",
                        Status = "Accepted",
                        Data = data
                    });
                }
                return BadRequest(new Response<string>
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = "Not Updated",
                    Message = "Validation Error",
                });
            }
            catch (Exception ex)
            {
                return NotFound(new Response<string>
                {
                    Code = HttpStatusCode.NotModified,
                    Status = "Not Modified",
                    Message = "Not Modified",
                    Data = ex.Message
                });
            }

        }

        [HttpDelete]
        [Route("SoftDeleteEmployee")]
        public async Task<IActionResult> SoftDeleteEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                var data = mapper.Map<Employee>(employee);
                await mediator.Send(new SoftDeleteEmployeeCommand(data));
                return Ok(new Response<Employee>
                {
                    Code = HttpStatusCode.OK,
                    Status = "Deleted",
                    Message = "Data Is Deleted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = "Not Deleted",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] EmployeeVM employee)
        {
            try
            {
                employee.CV?.RemoveFile("Documents");
                employee.Image?.RemoveFile("Images");
                var data = mapper.Map<Employee>(employee);
                await mediator.Send(new DeleteEmployeeCommand(data));
                return Ok(new Response<Employee>
                {
                    Code = HttpStatusCode.OK,
                    Status = "Deleted",
                    Message = "Data Is Deleted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = "Not Deleted",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }

    }
}
