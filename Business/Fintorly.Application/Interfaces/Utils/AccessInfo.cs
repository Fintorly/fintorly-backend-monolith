using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fintorly.Application.Interfaces.Utils
{
    public class AccessInfo
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Mentor Mentor { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public bool IsDeleted { get; set; }
    }
}

