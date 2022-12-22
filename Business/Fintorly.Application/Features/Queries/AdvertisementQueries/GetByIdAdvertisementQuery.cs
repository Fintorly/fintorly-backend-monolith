namespace Fintorly.Application.Features.Queries.AdvertisementQueries;

public class GetByIdAdvertisementQuery:IRequest<IResult>
{
    public Guid Id { get; set; }
}