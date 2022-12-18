using Fintorly.Application.Features.Commands.EmailCommands;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace Fintorly.Application.Interfaces.Utils
{
    public class MailManager : IMailService
    {
        IConfiguration Configuration { get; }
        EmailSendCommand _email;
        public MailManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _email = Configuration.GetSection("SmtpSettings").Get<EmailSendCommand>();

        }

        public async Task<IResult> SendEmail(EmailSendCommand emailSendDto)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Fahax", _email.SenderEmail));
            message.To.Add(new MailboxAddress(emailSendDto.ReceiverUserName, emailSendDto.ReceiverMail));
            message.Subject = emailSendDto.Subject;
            var body = new TextPart("plain")
            {
                Text = emailSendDto.Content
            };
            var client = new SmtpClient();
            client.Connect(_email.Server, 587, MailKit.Security.SecureSocketOptions.None);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_email.SenderEmail, _email.Password);
            //var multiPart = new Multipart();
            //multiPart.Add(body);
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

