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
            var result = await userManager.Users.AsNoTracking().ToListAsync();
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await userManager.FindByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(ModelState);
        }


        // PUT api/<UserController>/5
        [HttpPut("PutUser")]
        public async Task<IActionResult> PutUser([FromBody] ApplicationUser User)
        {
            var data = await userManager.FindByIdAsync(User.Id);
            if (data != null)
            {
                mapper.Map(User, data);
                IdentityResult result = await userManager.UpdateAsync(User);
                if (result.Succeeded)
                {
                    return Accepted(result);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        //// DELETE api/<UserController>/5
        //[HttpDelete("DeleteUser/{id}")]
        //public Task<IActionResult> DeleteUser(int id)
        //{
        //}
    }
}
