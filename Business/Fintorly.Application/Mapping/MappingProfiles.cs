using Fintorly.Application.Dtos.AdvertisementDto;
using Fintorly.Application.Dtos.AnswerDtos;
using Fintorly.Application.Dtos.MentorDtos;
using Fintorly.Application.Dtos.PortfolioDtos;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AdvertisementCommands;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Mapping;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        UserProfile();
        PortfolioProfile();
        AdvertisementProfile();
        AnswerProfile();
        MentorProfile();
    }
    
    private void UserProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        //CreateMap<CreateUserCommand, User>();
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<AccessToken, UserAndTokenDto>().ReverseMap();
    }

    private void MentorProfile()
    {
        CreateMap<RegisterCommand, Mentor>().ReverseMap();
        CreateMap<MentorDto, Mentor>().ReverseMap();
        CreateMap<UserDto, Mentor>().ReverseMap();
        CreateMap<Mentor, User>().ReverseMap();
    }
    private void PortfolioProfile()
    {
        CreateMap<Portfolio, PortfolioDto>();
    }
    private void AdvertisementProfile()
    {
        CreateMap<CreateAdvertisementCommand, Advertisement>();
        CreateMap<Advertisement,AdvertisementDto>().ReverseMap();
    }

    private void AnswerProfile()
    {
        CreateMap<AnswerDto, Answer>().ReverseMap();
    }
}