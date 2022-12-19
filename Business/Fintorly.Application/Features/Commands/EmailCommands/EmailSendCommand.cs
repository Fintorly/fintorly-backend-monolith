using System;
namespace Fintorly.Application.Features.Commands.EmailCommands
{
    public class EmailSendCommand : IRequest<IResult>
    {
        public string ReceiverUserName { get; set; }
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        //    "Server": "mail.fahax.com",
        //"Port": "587",
        //"SenderName": "Fahax",
        //"SenderEmail": "sistem@fahax.com",
        //"Username": "sistem@fahax.com",
        //"Password": "damwe7-serXiC2s3"
    }
}

