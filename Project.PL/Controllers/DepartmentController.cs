using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Model;
using Project.BLL.Services;
using Project.DAL.Entities;

namespace Project.PL.Controllers
{
    [Authorize(Roles = "Admin , Hr")]
    public class DepartmentController : Controller
    {
        private readonly IServicesRepo<Department> depart;
        private readonly IMapper mapper;

        public DepartmentController(IServicesRepo<Department> depart, IMapper mapper)
        {
            this.depart = depart;
            this.mapper = mapper;
        }

        public async Task<IActionResult> DepartmentServices()
        {
            var data = await depart.GetAsync();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(department);
                    await depart.CreateAsync(data);

                    return RedirectToAction("DepartmentServices", "Department");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(department);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await depart.GetByIdAsync(dep => dep.Department_Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await depart.GetByIdAsync(dep => dep.Department_Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(department);
                    await depart.UpdateAsync(data);
                    return RedirectToAction("DepartmentServices", "Department");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await depart.GetByIdAsync(dep => dep.Department_Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentVM department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(department);
                    await depart.DeleteAsync(data);
                    return RedirectToAction("DepartmentServices", "Department");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(department);
        }

    }
}
