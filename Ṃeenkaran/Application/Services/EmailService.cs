using System.Net;
using System.Net.Mail;

namespace Ṃeenkaran.Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpAsync(string toEmail, string otp)
        {
            await SendEmailAsync(
                toEmail,
                "Meenkaran Password Reset OTP",
                $@"
                    <h2>Password Reset OTP</h2>
                    <p>Your OTP is:</p>
                    <h1>{otp}</h1>
                    <p>This OTP will expire in 10 minutes.</p>
                ");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromEmail = _config["Email:FromEmail"];
            var appPassword = _config["Email:FromPassword"];

            if (string.IsNullOrWhiteSpace(fromEmail))
                throw new InvalidOperationException("Email:FromEmail missing in appsettings.json");

            if (string.IsNullOrWhiteSpace(appPassword))
                throw new InvalidOperationException("Email:FromPassword missing in appsettings.json");

            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, appPassword),
                EnableSsl = true
            };

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            await smtp.SendMailAsync(message);
        }
    }
}