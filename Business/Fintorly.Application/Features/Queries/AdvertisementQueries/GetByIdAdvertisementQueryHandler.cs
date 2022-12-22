namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetByIdAdvertisementQueryHandler : IRequestHandler<GetByIdAdvertisementQuery, IResult>
{
    private readonly IAdvertisementRepository _advertisementRepository;

    public GetByIdAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public async Task<IResult> Handle(GetByIdAdvertisementQuery request, CancellationToken cancellationToken)
    {
        return await _advertisementRepository.GetByIdAsync(request.Id);
    }
}