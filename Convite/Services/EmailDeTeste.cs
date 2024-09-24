using SendGrid.Helpers.Mail;
using SendGrid;

namespace Convite.Services
{
    public class EmailDeTeste
    {

        public async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("renato.lacerda@prodatamobility.com.br", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("renatoads1@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
