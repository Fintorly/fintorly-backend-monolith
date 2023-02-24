using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class UpdateAdvertisementCommandHandler : IRequestHandler<UpdateAdvertisementCommand, IResult>
{
    private IAdvertisementRepository _advertisementRepository;
    private IMapper _mapper;

    public UpdateAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
    {
        _advertisementRepository = advertisementRepository;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        var advertisement = _mapper.Map<Advertisement>(request);
        var result= await _advertisementRepository.UpdateAsync(advertisement);
        if (result)
            return await Result.SuccessAsync();
        return await Result.FailAsync();
    }
}