using Domain.Entities;

namespace Domain.Repositories
{
    public interface IKeyRepository
    {
        Task<IEnumerable<Key>> GetAllAsync();
        Task CreateAsync(Key key);
        Task UpdateAsync(Key key);
        Task<Key?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
    }
}
