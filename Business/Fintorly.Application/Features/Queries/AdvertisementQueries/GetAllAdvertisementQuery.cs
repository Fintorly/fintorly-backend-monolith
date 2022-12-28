using Fintorly.Application.Dtos.AdvertisementDto;

namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetAllAdvertisementQuery:IRequest<IResult<List<AdvertisementDto>>>
{
}