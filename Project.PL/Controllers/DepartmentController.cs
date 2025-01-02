using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Commands.DepartmentCommands;
using Project.BLL.Model;
using Project.BLL.Queries.DepartmentQueries;
using Project.DAL.Entities;

namespace Project.PL.Controllers
{
    [Authorize(Roles = "Admin , Hr")]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DepartmentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> DepartmentServices()
        {
            var data = await _mediator.Send(new GetAllDepartmentsQuery());
            var result = _mapper.Map<IEnumerable<DepartmentVM>>(data);
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
                    var data = _mapper.Map<Department>(department);
                    await _mediator.Send(new CreateDepartmentCommand(data));

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
            var data = await _mediator.Send(new GetDepartmentByIdQuery(id));
            var result = _mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _mediator.Send(new GetDepartmentByIdQuery(id));
            var result = _mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<Department>(department);
                    await _mediator.Send(new UpdateDepartmentCommand(data));
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
            var data = await _mediator.Send(new GetDepartmentByIdQuery(id));
            var result = _mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentVM department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<Department>(department);
                    await _mediator.Send(new DeleteDepartmentCommand(data));
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
