using VPN.Domain.Entities;

namespace VPN.Application.Services
{
    public interface IKeyService : IDisposable
    {
        Task<Key> CreateAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Key> GetByIdAsync(Guid id);
        Task<IEnumerable<Key>> GetByServerIdAsync(Guid id);
        Task<IEnumerable<Key>> GetAllAsync();
    }
}
