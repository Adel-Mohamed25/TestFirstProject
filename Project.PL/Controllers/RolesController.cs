using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BLL.Model;
using Project.DAL.Extend;

namespace Project.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public IActionResult GetRoles()
        {
            try
            {
                var data = roleManager.Roles.AsNoTracking().ToList();
                return View(data);
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await roleManager.CreateAsync(role);
                    return RedirectToAction("GetRoles");
                }
                return View(role);
            }
            catch (Exception ex)
            {
                return View($"Error : {ex.Message}");
            }
        }

        public async Task<IActionResult> Update(string id)
        {
            var data = await roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return View("Error", "Role not found.");
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole role)
        {
            try
            {
                var data = await roleManager.FindByIdAsync(role.Id);
                if (data == null)
                {
                    return View("Error", "Role not found.");
                }

                data.Name = role.Name;

                var result = await roleManager.UpdateAsync(data);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
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
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View(role);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var data = await roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return View($"Error : Not Found");
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole role)
        {
            try
            {
                await roleManager.DeleteAsync(role);
                return RedirectToAction("GetRoles");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string id)
        {
            var data = await roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return View($"Error : Not Found");
            }
            return View(data);
        }


        public async Task<IActionResult> AddOrRemoveUsers(string roleid)
        {
            var role = await roleManager.FindByIdAsync(roleid);
            ViewBag.RoleaNme = role.Name;
            var model = new List<UserInRoleVM>();

            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVM()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string roleid)
        {
            var role = await roleManager.FindByIdAsync(roleid);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].Id);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }
            return RedirectToAction("GetRoles");
        }

    }
}
