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

            smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Mail"], _configuration["Smtp:PasswMail"]),
                EnableSsl = true,
            };
        }

        public void Send(Membre membre)
        {
            String Url;
            if (Environment.GetEnvironmentVariable("SmtpUrl") == null)
            {
                Url = _configuration["Smtp:Url"];
            }
            else
            {
                Url = Environment.GetEnvironmentVariable("SmtpUrl");
            }
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:Mail"]),
                Subject = "Confirmation de votre compte MarketVinci",
                Body = "<h1>Market Vinci</h1>" +
                       "<p>Veuillez confirmer votre compte en cliquant sur le lien ci dessous.</p>" +
                       "<p><a href=\"" + Url + membre.Id + "\">Validez en cliquant ici</a></p>" +
                       "<p>Nous vous remercions d'utiliser Market Vinci.</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(membre.Email);
            smtpClient.Send(mailMessage);
        }
    }
}
