using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Domain.Repositories
{
    public interface IKeyRepository
    {
        Task<IEnumerable<Key>> GetAllAsync();
        Task AddAsync(Key key);
        Task UpdateAsync(Key key);
        Task<Key?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
    }
}
