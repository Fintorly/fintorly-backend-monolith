using System;
using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Mapping
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<UserDto, User>().ReverseMap();
			//CreateMap<CreateUserCommand, User>();
            CreateMap<RegisterCommand, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AccessToken, UserAndTokenDto>().ReverseMap();
        }
	}

	public class PortfolioProfile : Profile
	{
		public PortfolioProfile()
		{
			CreateMap<Portfolio, PortfolioDto>();
		}
	}
}

