using Fintorly.Domain.Entities;

namespace Fintorly.Application.Interfaces.Repositories;

public interface IPortfolioRepository:IGenericRepository<Portfolio>
{
    Task<IResult<Portfolio>> CreatePortfolioAsync(Guid userId, string portfolioName);
}