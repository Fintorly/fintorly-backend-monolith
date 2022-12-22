namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class DeleteAdvertisementCommand:IRequest<IResult>
{
    public Guid Id { get; set; }
}