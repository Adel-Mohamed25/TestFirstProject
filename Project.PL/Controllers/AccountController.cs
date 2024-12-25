using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Helper;
using Project.BLL.Model;
using Project.DAL.Extend;

namespace Project.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM signUpVM)
        {
            try
            {
                var data = mapper.Map<ApplicationUser>(signUpVM);
                var result = await userManager.CreateAsync(data, signUpVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            catch
            {
                return View(signUpVM);
            }

            return View(signUpVM);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            try
            {
                var data = await userManager.FindByEmailAsync(signInVM.Email);
                var result = await signInManager.PasswordSignInAsync(data.UserName, signInVM.Password, signInVM.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(signInVM);
            }
            return View(signInVM);
        }

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPassword)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(forgetPassword.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordresetlink = Url.Action("ResetPassword", "Account", new { Email = forgetPassword.Email, Token = token }, Request.Scheme);

                    MailSender.SendResetPasswordToMail(new EmailVM { Title = "Change password for Your Email", Message = passwordresetlink, Email = forgetPassword.Email });

                    return RedirectToAction("ConfirmForgetPassword");
                }
            }
            catch
            {
                return View(forgetPassword);
            }
            return View(forgetPassword);
        }

        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string? Email, string? Token)
        {
            if (Email != null && Token != null)
            {
                return View();
            }

            return RedirectToAction("ForgetPassword");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVM resetPassword)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(resetPassword.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ConfirmResetPassword");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(resetPassword);
                }
            }
            catch
            {
                return View(resetPassword);
            }

            return View(resetPassword);
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }


    }
}
