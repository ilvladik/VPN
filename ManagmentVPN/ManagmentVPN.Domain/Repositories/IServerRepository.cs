using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Domain.Repositories
{
    public interface IServerRepository
    {
        Task<IEnumerable<Server>> GetAllAsync();
        Task AddAsync(Server server);
        Task UpdateAsync(Server server);
        Task<Server?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
    }
}
