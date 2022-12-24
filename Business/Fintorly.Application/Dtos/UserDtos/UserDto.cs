using System;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Dtos.UserDtos
{
    public class UserDto
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
        public Portfolio Portfolio { get; set; }
        public Guid CurrentPortfolioId { get; set; }
        public DateTime LastLogin { get; set; }
    }
}

