using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IMailLogic
    {
        void Send(String email);
    }
    public class MailLogic : IMailLogic
    {
        private readonly SmtpClient smtpClient;
        public MailLogic()
        {
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("groupe11pfe@gmail.com", "groupe11pfe"),
                EnableSsl = true,
            };
        }

        public void Send(string email)
        {        
            smtpClient.Send("groupe11pfe@gmail.com", email, "TestSubject", "TestBody");
        }
    }
}
