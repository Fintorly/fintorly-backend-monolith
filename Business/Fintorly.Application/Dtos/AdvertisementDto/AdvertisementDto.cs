using Fintorly.Domain.Enums;

namespace Fintorly.Application.Dtos.AdvertisementDto;

public class AdvertisementDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public decimal Price { get; set; }
    public PackageType PackageType { get; set; }
}