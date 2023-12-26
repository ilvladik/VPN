using ManagmentVPN.Application.Dtos;
using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Application.Services
{
    public interface IServerService : IDisposable
    {
        Task<IEnumerable<Server>> GetAllAsync();
        Task AddAsync(string networkId, string apiPort, string apiPrefix);
        Task DeleteByIdAsync(Guid id);
        Task EnableAsync(Guid id);
        Task DisableAsync(Guid id);
    }
}
