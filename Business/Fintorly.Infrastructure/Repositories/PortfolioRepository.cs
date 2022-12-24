using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;

namespace Fintorly.Infrastructure.Repositories;

public class PortfolioRepository:GenericRepository<Portfolio>,IPortfolioRepository
{
    public PortfolioRepository(FintorlyContext context) : base(context)
    {
    }

    public async Task<IResult<Portfolio>> CreatePortfolioAsync(Guid userId, string portfolioName)
    {
        throw new NotImplementedException();
    }
}