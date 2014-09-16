using RockstarProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace RockstarProj.Services
{
    public class EmailService
    {

        //protected string FromAddress = "avista@rockstar-cafe.ca";
        //protected string FromPassword = "Rockstar1";

        protected string FromAddress = "rockstarcafe.videos@gmail.com";
        protected string FromPassword = "MrBrightside!1";
        //MrBrightside!1
        //rockstarcafe.videos@gmail.com

        public void SendMail(string toAddress, string subject, string body , List<HttpPostedFileBase> uploadedFiles)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(FromAddress);
            mailMsg.To.Add(toAddress);
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = true;
            mailMsg.Priority = MailPriority.Normal;

            foreach (HttpPostedFileBase item in uploadedFiles)
            {
                if (item != null)
                {
                    string fileName = Path.GetFileName(item.FileName);
                    mailMsg.Attachments.Add(new Attachment(item.InputStream,fileName));
                }
            }

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(FromAddress, FromPassword);
            client.Port = 25;
            //587
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mailMsg);
        }


        public string BecomeARockstarTextBuilder(InvolvedModel model)
        {
            string result = "";

            result = "<b>First Name<b>: " + model.FirstName + "<br/><br/><b>Last Name<b>: " + model.LastName + "<br/><br/><b>Email Address<b>: " + model.EmailAddress + "<br/><br/><b>Phone Number<b>: " + model.PhoneNumber +
                "<br/><br/><b/>Company/Project Name<b>: " + model.CompanyProjName + "<br/><br/><b>Company/Project Description<b>: " + model.CompanyProjDescription + "<br/><br/><b>Quanitifiable Metrics<b>: " + model.QuantifiableMetrics;

            return result;
        }

    }
}