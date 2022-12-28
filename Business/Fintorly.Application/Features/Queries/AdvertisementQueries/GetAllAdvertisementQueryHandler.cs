using Fintorly.Application.Dtos.AdvertisementDto;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetAllAdvertisementQueryHandler : IRequestHandler<GetAllAdvertisementQuery, IResult<List<AdvertisementDto>>>
{
    private IAdvertisementRepository _advertisement;
    private IMapper _mapper;

    public GetAllAdvertisementQueryHandler(IAdvertisementRepository advertisement, IMapper mapper)
    {
        _advertisement = advertisement;
        _mapper = mapper;
    }

    public async Task<IResult<List<AdvertisementDto>>> Handle(GetAllAdvertisementQuery request, CancellationToken cancellationToken)
    {
        var result=await _advertisement.GetAllAsync();
        var advertisementsDtos = _mapper.Map<List<AdvertisementDto>>(result);
        return Result<List<AdvertisementDto>>.Success(advertisementsDtos);
    }
}