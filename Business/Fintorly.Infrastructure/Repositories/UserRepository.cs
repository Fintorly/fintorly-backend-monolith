using System;
using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;

namespace Fintorly.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private FintorlyContext _context;
        public UserRepository(FintorlyContext context) : base(context)
        {
            _context = context;
        }
    }
}

