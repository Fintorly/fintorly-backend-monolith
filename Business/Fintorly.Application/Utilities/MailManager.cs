using System.Net;
using Fintorly.Application.Configurations;
using Fintorly.Application.Features.Commands.EmailCommands;
using Fintorly.Application.Interfaces.Utils;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Fintorly.Application.Utilities
{
    public class MailManager : IMailService
    {
        MailConfiguration _mailConfiguration;

        public MailManager(IConfiguration configuration)
        {
            _mailConfiguration = configuration.GetSection("SmtpSettings").Get<MailConfiguration>();
        }

        public async Task<IResult> SendEmail(EmailSendCommand emailSendCommand)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_mailConfiguration.SenderName, _mailConfiguration.SenderEmail));
            message.To.Add(new MailboxAddress(emailSendCommand.ReceiverUserName, emailSendCommand.ReceiverMail));
            message.Subject = emailSendCommand.Subject;
            var body = new TextPart("plain")
            {
                Text = emailSendCommand.Content
            };
            var client = new SmtpClient();
            client.Connect(_mailConfiguration.Server, 587,false);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_mailConfiguration.UserName, _mailConfiguration.Password);
            message.Body = body;
            client.Send(message);
            client.Dispose();
            return Result.Success("Mail Başarıyla Gönderildi");
        }

        public Task<IResult> SendLandingEmail(LandingEmailCommand landingEmailDto)
        {
            throw new NotImplementedException();
        }
    }
}