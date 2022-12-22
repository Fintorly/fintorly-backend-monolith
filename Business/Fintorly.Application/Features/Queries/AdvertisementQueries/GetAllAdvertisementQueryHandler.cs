using Fintorly.Application.Dtos.AdvertisementDto;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetAllAdvertisementQueryHandler : IRequestHandler<GetAllAdvertisementQuery, IResult>
{
    private IAdvertisementRepository _advertisement;
    private IMapper _mapper;

    public GetAllAdvertisementQueryHandler(IAdvertisementRepository advertisement, IMapper mapper)
    {
        _advertisement = advertisement;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(GetAllAdvertisementQuery request, CancellationToken cancellationToken)
    {
        var result=await _advertisement.GetAllAsync();
        var advertisements =result.Data as List<Advertisement>;
        var advertisementsDto = _mapper.Map<List<AdvertisementDto>>(advertisements);
        result.Data = advertisementsDto;
        return result;
    }
}