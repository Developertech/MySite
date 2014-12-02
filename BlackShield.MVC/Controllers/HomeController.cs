using MySite.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MySite.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Contact = "SEND ME AN EMAIL BY FILLING OUT THE FORM";
            return View();                               
        }
                

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            
            if (ModelState.IsValid)
            {

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("NoReplyDevTech@gmail.com", "walkingdead123");


                    string body = string.Format(
                        "First Name: {0}\nLast Name: {1}\n Company: {2}\n Email: {3}\nComment: {4}",
                        c.FirstName,
                        c.LastName,
                        c.Company,
                        c.Email,
                        c.Comment
                    );

                    var message = new MailMessage();
                    message.To.Add("sabirhfoux@gmail.com");
                    message.From = new MailAddress(c.Email, c.Name);
                    message.Subject = String.Format("Contact Request From: {0} ", c.Name);
                    message.Body = body;
                    message.IsBodyHtml = false;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }

                }
                
            }
            ViewBag.Contact = "THANKS, I'VE RECEIVED YOUR MESSAGE";
            return Redirect("#contact");
            
        }
	}
}