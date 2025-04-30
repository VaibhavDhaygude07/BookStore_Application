using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_configuration["SMTP:Host"])
            {
                Port = int.Parse(_configuration["SMTP:Port"]),
                Credentials = new NetworkCredential(_configuration["SMTP:Username"], _configuration["SMTP:Password"]),
                EnableSsl = bool.Parse(_configuration["SMTP:EnableSSL"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SMTP:FromEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
