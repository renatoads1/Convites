using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using Convite.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Convite.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly Settings _sendGridSettings;

        public EmailSender(IOptions<Settings> sendGridSettings)
        {
            _sendGridSettings = sendGridSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var from = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName);
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);
             await client.SendEmailAsync(message);
        }
    }
}
