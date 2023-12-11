
using Domain.Repositories;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IServerRepository Servers { get; }
        IKeyRepository Keys { get; }
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}
