using Fintorly.Domain.Enums;

namespace Fintorly.Application.Features.Commands.AdvertisementCommands;

public class UpdateAdvertisementCommand : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public decimal Price { get; set; }
    public PackageType PackageType { get; set; }
}