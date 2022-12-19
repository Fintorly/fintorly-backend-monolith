using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, IResult>
{
    private IAdvertisement _advertisement;
    private IMapper _mapper;

    public CreateAdvertisementCommandHandler(IAdvertisement advertisement, IMapper mapper)
    {
        _advertisement = advertisement;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Title))
            return Result.Fail("Title alanı boş olamaz");
        if (string.IsNullOrEmpty(request.Content))
            return Result.Fail("Content alanı boş olamaz");
        if (request.Price <= 0)//Price 0'dan büyükse
            return Result.Fail("Content alanı boş olamaz");

        var advertisement = _mapper.Map<Advertisement>(request);
        var result = await _advertisement.AddAsync(advertisement);
        return result;
    }
}