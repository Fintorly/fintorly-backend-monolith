using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;

namespace Fintorly.Application.Interfaces.Repositories;

public interface IPortfolioRepository:IGenericRepository<Portfolio>
{
    Task<IResult<Portfolio>> CreatePortfolioAsync(Mentor mentor);
    Task<IResult<Portfolio>> CreatePortfolioAsync(User mentor);
}