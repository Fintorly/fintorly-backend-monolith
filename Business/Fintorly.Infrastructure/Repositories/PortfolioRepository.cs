using Fintorly.Domain.Common;
using Fintorly.Domain.Entities;
using Fintorly.Domain.Enums;
using Fintorly.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories;

public class PortfolioRepository:GenericRepository<Portfolio>,IPortfolioRepository
{
    private FintorlyContext _context;
    public PortfolioRepository(FintorlyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IResult<Portfolio>> CreatePortfolioAsync(Mentor mentor)
    {
        var mentorExist=await _context.Users.SingleOrDefaultAsync(a => a.Id == mentor.Id);

            Portfolio portfolio = new Portfolio()
            {
                Mentor = mentor,
                MentorId = mentor.Id,
                TotalPrice = 0,
                TotalPriceUser24Hour = 0,
                TotalPriceChange = 0,
                TotalPriceChangePercent = 0,
                Name = "My Portfolio",
                PortfolioTokens = new List<PortfolioToken>()
            };
            mentorExist.Portfolios.Add(portfolio);
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return Result<Portfolio>.Success(portfolio);
    }
    
    public async Task<IResult<Portfolio>> CreatePortfolioAsync(User user)
    {
        var userExist=await _context.Users.SingleOrDefaultAsync(a => a.Id == user.Id);
        Portfolio portfolio = new Portfolio()
        {
            User = user,
            UserId = user.Id,
            TotalPrice = 0,
            TotalPriceUser24Hour = 0,
            TotalPriceChange = 0,
            TotalPriceChangePercent = 0,
            Name = "My Portfolio",
            PortfolioTokens = new List<PortfolioToken>()
        };
        userExist.Portfolios.Add(portfolio);
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return Result<Portfolio>.Success(portfolio);
    }
}