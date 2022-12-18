using System;
using Fintorly.Domain.Common;

namespace Fintorly.Domain.Entities
{
    public class VerificationCode : BaseEntity, IEntity
    {
        public string EmailAddress { get; set; }
        public bool IsMailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; } = false;
        public string PhoneCode { get; set; }
        public string MailCode { get; set; }
        public DateTime VerificationCodeValidDate { get; set; }
    }
}

