using SharedKernel.Dtos.Server;

namespace VPN.Application.Services
{
    public interface IServerService : IDisposable
    {
        Task<ServerDtoResponse> AddAsync(ServerDtoAddRequest serverDtoAddRequest);
        Task<IEnumerable<ServerDtoResponse>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
