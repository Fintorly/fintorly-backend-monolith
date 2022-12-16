using System;
namespace Fintorly.Domain.Common
{
    public interface IRepository<T>
	{
        IUnitOfWork UnitOfWork { get; }
    }
}

