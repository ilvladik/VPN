using Domain.Entities;

namespace Domain.Repositories
{
    public interface IServerRepository
    {
        Task<IEnumerable<Server>> GetAllAsync();
        Task AddAsync(Server server);
        Task<Server?> GetByIdAsync(Guid id);
        Task UpdateAsync(Server server);
        Task DeleteByIdAsync(Guid id);
    }
}
