using MiguelPortFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace MiguelPortFinal.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactFormViewModel());
        }
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["EmailSent"] = false;
                return CurrentUmbracoPage();
            }

            var body = "<h4>New message from:</h4><p>{0}</p><h4>Email:</h4><p>{1}</p><h4>Message:</h4><p>{2}</p>";
            MailMessage message = new MailMessage();
            message.To.Add("contact@mywayphotoss.com");
            message.Subject = "New contact request from your website";
            message.Body = string.Format(body, model.Name, model.Email, model.Message);
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
            TempData["EmailSent"] = true;

            return RedirectToCurrentUmbracoPage();
        }
    }
}