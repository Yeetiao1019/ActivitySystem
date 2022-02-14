using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ActivitySystem.Common
{
    public class EmailSender : IEmailSender
    {
        private string Host;
        private int Port;
        private bool IsEnableSSL;
        private string UserName;
        private string Password;
        private SmtpClient SmtpClient;
        public EmailSender(string host, int port, bool isEnableSSL, string userName, string password)
        {
            this.Host = host;
            this.Port = port;
            this.IsEnableSSL = isEnableSSL;
            this.UserName = userName;
            this.Password = password;

            SmtpClient = new SmtpClient
            {
                Port = Port,
                Host = Host,
                EnableSsl = IsEnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password)
            };
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return SmtpClient.SendMailAsync(UserName, email, subject, htmlMessage);
        }
    }
}
