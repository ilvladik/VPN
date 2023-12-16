
using VPN.Domain.Entities;

namespace VPN.Application.Services
{
    public interface IServerService : IDisposable
    {
        Task<Server> AddAsync(string networkId, int apiPort, string apiPrefix);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Server>> GetAllAsync();
        Task<Server> GetByIdAsync(Guid id);
    }
}
