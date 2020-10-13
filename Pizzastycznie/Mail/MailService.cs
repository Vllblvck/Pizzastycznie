using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Pizzastycznie.Mail
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly SmtpSettings _smtpSettings;

        public MailService(ILogger<MailService> logger, IOptions<SmtpSettings> smtpSettings)
        {
            _logger = logger;
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Preparing e-mail message");
                var message = new MimeMessage
                {
                    From = {new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail)},
                    To = {new MailboxAddress(email)},
                    Subject = subject,
                    Body = new TextPart(TextFormat.Html)
                    {
                        Text = body
                    }
                };

                _logger.LogInformation("Sending e-mail");
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await smtpClient.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                    await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await smtpClient.SendAsync(message);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while sending email: {ex.Message}");
            }
        }
    }
}