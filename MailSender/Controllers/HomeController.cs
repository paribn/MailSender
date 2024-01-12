using MailSender.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace MailSender.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SendEmail(EmailEntity objEMail)
        {

            var app = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var username = app.GetValue<string>("EmailConfig:Username");
            var pass = app.GetValue<string>("EmailConfig:Password");
            var Host = app.GetValue<string>("EmailConfig:Host");
            var Port = app.GetValue<int>("EmailConfig:Port");
            var FromEmail = app.GetValue<string>("EmailConfig:FromEmail");
            var subject = app.GetValue<string>("EmailConfig:Subject");
            var mess = app.GetValue<string>("EmailConfig:Message");


            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmail);
            mailMessage.To.Add(objEMail.ToEmail.ToString());
            mailMessage.Subject = subject;
            mailMessage.Body = mess;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient(Host);

            try
            {

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(username, pass);
                smtpClient.Host = Host;
                smtpClient.Port = Port;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                smtpClient.Dispose();
            }

            return View("Index");

        }
    }
}