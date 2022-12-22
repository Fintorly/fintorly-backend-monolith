using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;

namespace Fintorly.Infrastructure.Repositories;

public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
{
    public AdvertisementRepository(FintorlyContext context) : base(context)
    {
    }
}