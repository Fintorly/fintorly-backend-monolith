using Fintorly.Application.Dtos.AdvertisementDto;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetByIdAdvertisementQueryHandler : IRequestHandler<GetByIdAdvertisementQuery, IResult<AdvertisementDto>>
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private IMapper _mapper;
    public GetByIdAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
    {
        _advertisementRepository = advertisementRepository;
        _mapper = mapper;
    }

    public async Task<IResult<AdvertisementDto>> Handle(GetByIdAdvertisementQuery request, CancellationToken cancellationToken)
    {
        var advertisement= await _advertisementRepository.GetByIdAsync(request.Id);
        var dto = _mapper.Map<AdvertisementDto>(advertisement);
        return await Result<AdvertisementDto>.SuccessAsync(dto);
    }
}