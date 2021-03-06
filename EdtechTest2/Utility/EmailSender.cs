using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;
        public EmailSender(IOptions<EmailOptions> options)
        {
            // congigure to get the values from appsetting.json
            emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string Message)
        {
            return Execute(emailOptions.SendGridKey, subject, Message, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("sachinshrestha483@gmail.com", "Edtech App");
            var to = new EmailAddress(email, "Example User");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, message, "");// for plain text
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);// for html text

            return client.SendEmailAsync(msg);
        }
    }
}
