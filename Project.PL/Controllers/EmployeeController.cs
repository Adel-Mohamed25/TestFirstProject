using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.BLL.Repository;
using Project.BLL.Services;
using Project.DAL.Entities;


namespace Project.PL.Controllers
{
    [Authorize(Roles = "Employee,Admin , Hr")]
    public class EmployeeController : Controller
    {
        private readonly IServicesRepo<Employee> emp;
        private readonly IServicesRepo<Department> depart;
        private readonly IMapper mapper;
        private readonly ICityRepo city;
        private readonly IDistrictRepo district;
        private readonly EmployeeRepo repo;

        public EmployeeController(IServicesRepo<Employee> emp, IServicesRepo<Department> depart, IMapper mapper, ICityRepo city, IDistrictRepo district, EmployeeRepo repo)
        {
            this.emp = emp;
            this.depart = depart;
            this.mapper = mapper;
            this.city = city;
            this.district = district;
            this.repo = repo;
        }

        public async Task<IActionResult> EmployeeServices(string searchvalue)
        {
            if (searchvalue is null)
            {
                var data = await emp.GetAsync(emp => emp.IsActive == true && emp.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                var data = await emp.GetAsync(emp => (emp.Employee_Fname + emp.Employee_Lname).Contains(searchvalue) && emp.IsActive == true && emp.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }

        }

        public async Task<IActionResult> Create()
        {
            var data = await depart.GetAsync();
            ViewBag.departmentlist = new SelectList(data, "Department_Id", "Department_Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.CVName = employee.CV?.UploadFile("wwwroot", "Documents");
                    employee.ImageName = employee.Image?.UploadFile("wwwroot", "Images");
                    var data = mapper.Map<Employee>(employee);
                    await emp.CreateAsync(data);
                    return RedirectToAction("EmployeeServices", "Employee");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            var departments = await depart.GetAsync();
            ViewBag.departmentlist = new SelectList(departments, "Department_Id", "Department_Name");
            return View(employee);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await emp.GetByIdAsync(emp => emp.Employee_Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var data = await emp.GetByIdAsync(emp => emp.Employee_Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(employee);
                    await repo.SoftDeleteAsync(data);
                    return RedirectToAction("EmployeeServices", "Employee");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var departments = await depart.GetAsync();
            ViewBag.departmentlist = new SelectList(departments, "Department_Id", "Department_Name");
            var data = await emp.GetByIdAsync(emp => emp.Employee_Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //employee.CVName = employee.CV.UploadFile("Docs");
                    //employee.ImageName = employee.Image.UploadFile("Imgs");
                    var data = mapper.Map<Employee>(employee);
                    await emp.UpdateAsync(data);
                    return RedirectToAction("EmployeeServices", "Employee");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await emp.GetByIdAsync(emp => emp.Employee_Id == id && emp.IsDeleted == true);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.CV?.RemoveFile("wwwroot", "Documents");
                    employee.Image?.RemoveFile("wwwroot", "Images");
                    var data = mapper.Map<Employee>(employee);
                    await emp.DeleteAsync(data);
                    return RedirectToAction("DeletedEmployee", "Employee");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(employee);
        }

        public async Task<IActionResult> DeletedEmployee()
        {
            var data = await emp.GetAsync(emp => emp.IsDeleted == true);
            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnEmployee(int id)
        {
            var data = await emp.GetByIdAsync(emp => emp.Employee_Id == id);

            var result = mapper.Map<EmployeeVM>(data);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnEmployee(EmployeeVM employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(employee);
                    await repo.ReturnAsync(data);
                    return RedirectToAction("EmployeeServices");
                }
            }
            catch
            {
                return View(employee);
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<JsonResult> GetCitiesByCountryId(int countryid)
        {
            var data = await city.GetAsync(cit => cit.Country_Id == countryid);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictsByCityId(int cityid)
        {
            var data = await district.GetAsync(dis => dis.City_Id == cityid);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }

    }
}
