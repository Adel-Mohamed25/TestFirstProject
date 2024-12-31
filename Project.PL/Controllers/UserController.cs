using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Extend;


namespace Project.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetUsers()
        {
            var data = await userManager.Users.AsNoTracking().ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public async Task<IActionResult> Update(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser model)
        {
            try
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                mapper.Map(model, user);

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser model)
        {
            try
            {

                var result = await userManager.DeleteAsync(model);
                return RedirectToAction("GetUsers");
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }
        }

    }
}
