using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class SendEmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task SendEmailUsingSendGrid(EmailAddress to)
        {

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("TP055753@mail.apu.edu.my", "ADMIN");
            var subject = "Reset Password";
            //var to = new EmailAddress("lai.khaiming@gmail.com", "Khai Meng");
            var plainTextContent = "Your Password Has Reset. Please login with your registered Email. Your new Password is (Ic Number)_Reset";
            var htmlContent = "<strong>Your Password Has Reset. Please login with your registered Email. Your new Password is (Ic Number)_Reset</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
