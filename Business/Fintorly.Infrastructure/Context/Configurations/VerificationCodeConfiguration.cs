using System;
using Fintorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintorly.Infrastructure.Context.Configurations
{
	public class VerificationCodeConfiguration:IEntityTypeConfiguration<VerificationCode>
	{
		public VerificationCodeConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
			builder.HasKey(a => a.Id);

			builder.Property(a => a.MailCode).IsRequired(false);
			builder.Property(a => a.PhoneCode).IsRequired(false);
			builder.Property(a => a.IpAddress).IsRequired(false);
			builder.Property(a => a.OsType).IsRequired(false);
			builder.Property(a => a.PhoneModel).IsRequired(false);
			
			builder.ToTable("VerificationCodes");
		}
    }
}

