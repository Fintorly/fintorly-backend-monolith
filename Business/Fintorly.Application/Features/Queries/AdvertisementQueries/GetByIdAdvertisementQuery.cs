using Fintorly.Application.Dtos.AdvertisementDto;

namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetByIdAdvertisementQuery:IRequest<IResult<AdvertisementDto>>
{
    public Guid Id { get; set; }
}