using VPN.Domain.Entities;

namespace VPN.Domain.Repositories;

public interface IKeyRepository
{
    Task<Key?> GetByIdAsync(Guid id);
    Task<IEnumerable<Key>> GetAllAsync();
    Task<IEnumerable<Key>> GetByServerIdAsync(Guid serverId);
    Task AddAsync(Key key);
    Task DeleteByIdAsync(Guid id);
}
