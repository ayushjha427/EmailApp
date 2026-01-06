using Microsoft.AspNetCore.Mvc;
using EmailCommunicator.Services;
using EmailCommunicator.Models;

namespace EmailCommunicator.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailService _email;

        public EmailController(EmailService email)
        {
            _email = email;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(EmailModel model)
        {
            _email.Send(model.To, model.Subject, model.Body);
            ViewBag.Msg = "Email sent successfully ✅";
            return View();
        }

        public IActionResult Inbox()
        {
            var mails = _email.GetInbox();
            return View(mails);
        }
    }
}