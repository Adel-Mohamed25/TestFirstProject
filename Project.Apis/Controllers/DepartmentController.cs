using Microsoft.AspNetCore.Mvc;
using Project.BLL.Commands.DepartmentCommands;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.BLL.Queries.DepartmentQueries;
using Project.DAL.Entities;
using System.Net;

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Hr")]
    public class DepartmentController : BaseController
    {
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var data = await mediator.Send(new GetAllDepartmentsQuery());
                var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
                return Ok(new Response<IEnumerable<DepartmentVM>>()
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
        [Route("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var data = await mediator.Send(new GetDepartmentByIdQuery(id));
                var result = mapper.Map<DepartmentVM>(data);
                return Ok(new Response<DepartmentVM>()
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
        [Route("PostDepartment")]
        public async Task<IActionResult> PostDepartment(DepartmentVM Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(Department);
                    await mediator.Send(new CreateDepartmentCommand(data));
                    return Ok(new Response<Department>()
                    {
                        Code = HttpStatusCode.Created,
                        Status = "Create",
                        Message = "Data Saved",
                        Data = data
                    });
                }
                return BadRequest(new Response<string>()
                {
                    Code = HttpStatusCode.BadRequest,
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
        [Route("PutDepartment")]
        public async Task<IActionResult> PutDepartment(DepartmentVM Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(Department);
                    await mediator.Send(new UpdateDepartmentCommand(data));
                    return Ok(new Response<Department>
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
        [Route("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(DepartmentVM Department)
        {
            try
            {
                var data = mapper.Map<Department>(Department);
                await mediator.Send(new DeleteDepartmentCommand(data));
                return Ok(new Response<Department>
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
