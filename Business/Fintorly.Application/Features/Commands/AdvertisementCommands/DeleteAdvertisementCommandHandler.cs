namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommand,IResult>
{
    private IAdvertisementRepository _advertisementRepository;

    public DeleteAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public async Task<IResult> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
    {
        var result =await _advertisementRepository.DeleteByIdAsync(request.Id);
        if (result)
            return await Result.SuccessAsync();
        return await Result.FailAsync();
    }
}