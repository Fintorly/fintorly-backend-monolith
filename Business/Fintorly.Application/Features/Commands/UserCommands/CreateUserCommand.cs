using System;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
        public bool IsEmailAddressVerified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Password { get; set; }
    }
}

