using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Extend;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        // GET: api/<UserController>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await userManager.Users.AsNoTracking().ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await userManager.FindByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("User Not Found.");
        }


        // PUT api/<UserController>/5
        [HttpPut("PutUser")]
        public async Task<IActionResult> PutUser([FromBody] ApplicationUser User)
        {
            if (ModelState.IsValid)
            {
                var data = await userManager.FindByIdAsync(User.Id);
                if (data == null)
                {
                    return NotFound("User Not Found.");
                }
                mapper.Map(User, data);
                IdentityResult result = await userManager.UpdateAsync(data);

                if (result.Succeeded)
                {
                    return Ok("User Updated Successfully.");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User Not Found.");
            }

            IdentityResult result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User Deleted Successfully .");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
