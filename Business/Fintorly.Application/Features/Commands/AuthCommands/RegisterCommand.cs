using System;
using Fintorly.Application.Dtos.AnswerDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AuthCommands
{
    public class RegisterCommand : IRequest<IResult<UserAndTokenDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Birth { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsMentor { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
    }
}