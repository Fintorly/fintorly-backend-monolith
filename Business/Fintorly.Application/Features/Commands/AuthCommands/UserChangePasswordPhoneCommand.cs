﻿namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class UserChangePasswordPhoneCommand : IRequest<IResult>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ReTypePassword { get; set; }
    }
}

