using ManagmentVPN.Application.Dtos;

namespace ManagmentVPN.Application.Services
{
    public interface IServerService : IDisposable
    {
        Task<IEnumerable<ServerDto>> GetAllAsync();
        Task AddAsync(string networkId, string apiPort, string apiPrefix);
        Task DeleteByIdAsync(Guid id);
        Task Enable(Guid id);
        Task Disable(Guid id);
    }
}
