using Fintorly.Application.Dtos.AdvertisementDto;
using Fintorly.Application.Features.Commands.AdvertisementCommands;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Mapping;

public class AdvertisementProfile : Profile
{
    public AdvertisementProfile()
    {
        CreateMap<CreateAdvertisementCommand, Advertisement>();
        CreateMap<Advertisement,AdvertisementDto>().ReverseMap();
    }
}