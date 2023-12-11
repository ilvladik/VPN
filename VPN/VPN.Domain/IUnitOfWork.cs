using VPN.Domain.Repositories;

namespace VPN.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IServerRepository Servers { get; }
        IKeyRepository Keys { get; }
        Task SaveChangesAsync();
    }
}
