using System;
namespace Fintorly.Application.Features.Commands.AuthCommands
{
	public class RegisterCommand:IRequest<IResult>
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Birth { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}

