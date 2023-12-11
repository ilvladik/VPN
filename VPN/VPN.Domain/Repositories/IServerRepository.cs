using VPN.Domain.Entities;

namespace VPN.Domain.Repositories
{
    public interface IServerRepository
    {
        Task<Server?> GetByIdAsync(Guid id);
        Task<IEnumerable<Server>> GetAllAsync();
        Task<Server> AddAsync(Server server);
        Task DeleteByIdAsync(Guid id);
    }
}
