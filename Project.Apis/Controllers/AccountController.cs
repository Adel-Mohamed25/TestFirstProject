using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.BLL.Model;
using Project.DAL.Extend;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Project.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpVM UserRegister)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<ApplicationUser>(UserRegister);
                IdentityResult result = await userManager.CreateAsync(user, UserRegister.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                    return Ok($"Message: {HttpStatusCode.Created}");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(SignInVM UserLogIn)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(UserLogIn.Email);
                if (user != null)
                {
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("Error", "The account is temporarily locked due to incorrect login attempts. Please try again after 5 minutes.");
                        return BadRequest(ModelState);
                    }

                    bool logInFound = await userManager.CheckPasswordAsync(user, UserLogIn.Password);

                    if (logInFound)
                    {
                        // add tokenExpiration for JWT payload.
                        var tokenExpiration = DateTime.Now.AddHours(1);

                        // Add Claims for JWT payload.
                        var userClaims = new List<Claim>();
                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));

                        var userRoles = await userManager.GetRolesAsync(user);
                        foreach (var role in userRoles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        // Add Credentials for JWT payload.
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
                        var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        // Desidn Token for JWT.
                        JwtSecurityToken token = new JwtSecurityToken(

                            issuer: configuration["JWT:IssuerUrl"],
                            audience: configuration["JWT:AudienceUrl"],
                            expires: tokenExpiration,
                            claims: userClaims,
                            signingCredentials: signingCredential

                            );


                        return Ok(new
                        {
                            createtoken = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = tokenExpiration // another way => token.ValidTo
                        });

                    }

                    ModelState.AddModelError("Error", "UserName Or Password InValid");
                }
            }
            return BadRequest(ModelState);
        }


    }
}
