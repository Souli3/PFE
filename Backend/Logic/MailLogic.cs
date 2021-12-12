using Backend.Models;
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
        void Send(Membre membre);
    }
    public class MailLogic : IMailLogic
    {
        private readonly SmtpClient smtpClient;
        private IConfiguration _configuration;
        public MailLogic(IConfiguration configuration)
        {
            _configuration = configuration;

            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_configuration["Domains:Mail"], _configuration["Domains:PasswMail"]),
                EnableSsl = true,
            };
        }

        public void Send(Membre membre)
        {        
            smtpClient.Send(_configuration["Domains:Mail"], membre.Email, "TestSubject", "TestBody");
        }
    }
}
