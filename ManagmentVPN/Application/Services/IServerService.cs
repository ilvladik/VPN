using Domain.Entities;
using SharedKernel.Dtos.Server;

namespace Application.Services
{
    public interface IServerService : IDisposable
    {
        Task<IEnumerable<Server>> GetAllAsync();
        Task AddAsync(ServerDtoAddRequest request);
        Task DeleteByIdAsync(Guid id);
        Task ChangeStateAsync(Guid id, ServerState state);
    }
}
