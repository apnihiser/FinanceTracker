using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Web.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private MailJetOptions _mailJetOptions;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _mailJetOptions = _config.GetSection("MailJet").Get<MailJetOptions>();

            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey) { };

            MailjetRequest request = new MailjetRequest { Resource = Send.Resource }
               .Property(Send.FromEmail, "apnihiser@protonmail.com")
               .Property(Send.FromName, "Finance Tracker Admin")
               .Property(Send.Subject, subject)
               .Property(Send.HtmlPart, htmlMessage)
               .Property(Send.Recipients, new JArray {
                    new JObject 
                    {
                        {"Email", email}
                    }
                   });

                MailjetResponse response = await client.PostAsync(request);
        }
    }
}
