using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.BLL.Services;
using Project.DAL.ConnectionData;
using Project.DAL.Entities;

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IServicesRepo<Department> department;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public DepartmentController(IServicesRepo<Department> department, IMapper mapper, ApplicationDbContext db)
        {
            this.department = department;
            this.mapper = mapper;
            this.db = db;
        }


        [HttpGet]
        [Route("~/api/Department/GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var data = await department.GetAsync();
                var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
                //return Ok(new ApiResponse<IEnumerable<DepartmentVM>>()
                //{
                //    Code = 200,
                //    Status = "Ok",
                //    Message = "Succed",
                //    Data = result
                //});

                return Ok(result);
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
        [Route("~/api/Department/GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var data = await department.GetByIdAsync(dep => dep.Department_Id == id);
                var result = mapper.Map<DepartmentVM>(data);
                //return Ok(new ApiResponse<DepartmentVM>()
                //{
                //    Code = 200,
                //    Status = "Ok",
                //    Message = "Succed",
                //    Data = result
                //});

                return Ok(result);
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
        [Route("~/api/Department/PostDepartment")]
        public async Task<IActionResult> PostDepartment(DepartmentVM Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(Department);
                    await department.CreateAsync(data);
                    //return Ok(new ApiResponse<Department>()
                    //{
                    //    Code = 201,
                    //    Status = "Create",
                    //    Message = "Data Saved",
                    //    Data = data
                    //});

                    return Ok();
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
        [Route("~/api/Department/PutDepartment")]
        public async Task<IActionResult> PutDepartment(DepartmentVM Department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(Department);
                    await department.UpdateAsync(data);
                    //return Ok(new ApiResponse<Department>
                    //{
                    //    Code = 202,
                    //    Message = "Updated",
                    //    Status = "Accepted",
                    //    Data = data
                    //});
                    return Ok();
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
        [Route("~/api/Department/DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(DepartmentVM Department)
        {
            try
            {
                var data = mapper.Map<Department>(Department);
                await department.DeleteAsync(data);
                //return Ok(new ApiResponse<Department>
                //{
                //    Code = 200,
                //    Status = "Deleted",
                //    Message = "Data Is Deleted",
                //    Data = data
                //});

                return Ok();
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
