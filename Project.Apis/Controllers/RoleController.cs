using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        // GET: api/<RoleController>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _roleManager.Roles.AsNoTracking().ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                return Ok(role);
            }
            return NotFound("Role Not Found");
        }

        // POST api/<RoleController>
        [HttpPost("PostRole")]
        public async Task<IActionResult> PostRole([FromBody] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        // PUT api/<RoleController>/5
        [HttpPut("PutRole")]
        public async Task<IActionResult> PutRole([FromBody] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var data = await _roleManager.FindByIdAsync(role.Id);
                if (data == null)
                {
                    return NotFound("Role Not Found");
                }

                mapper.Map(role, data);
                IdentityResult result = await _roleManager.UpdateAsync(data);
                if (result.Succeeded)
                {
                    return Ok("Role Updated Successfully.");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role Not Found.");
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role Deleted Successfully .");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
