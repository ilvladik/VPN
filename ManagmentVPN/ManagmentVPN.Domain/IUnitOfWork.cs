using ManagmentVPN.Domain.Repositories;

namespace ManagmentVPN.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IServerRepository Servers { get; }
        IKeyRepository Keys { get; }
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}
