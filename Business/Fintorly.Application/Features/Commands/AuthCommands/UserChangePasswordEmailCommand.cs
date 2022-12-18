﻿namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class UserChangePasswordEmailCommand : IRequest<IResult>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ReTypePassword { get; set; }
    }
}

