using Microsoft.AspNetCore.Mvc;
using Project.BLL.Helper;
using Project.BLL.Model;

namespace Project.PL.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(EmailVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Message = MailSender.SendMessageToMail(model);
                    return RedirectToAction("SendEmail");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

    }
}
