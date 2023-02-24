using System;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, IResult>
{
    private IAdvertisementRepository _advertisement;
    private IMapper _mapper;

    public CreateAdvertisementCommandHandler(IAdvertisementRepository advertisement, IMapper mapper)
    {
        _advertisement = advertisement;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Title))
            return await Result.FailAsync("Title alanı boş olamaz");
        if (string.IsNullOrEmpty(request.Content))
            return await Result.FailAsync("Content alanı boş olamaz");
        if (request.Price <= 0)//Price 0'dan büyükse
            return await Result.FailAsync("Content alanı boş olamaz");

        var advertisement = _mapper.Map<Advertisement>(request);
        var result = await _advertisement.AddAsync(advertisement);
        return await Result.SuccessAsync();
    }


}