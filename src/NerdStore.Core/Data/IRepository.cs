using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot //Base de um repository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
