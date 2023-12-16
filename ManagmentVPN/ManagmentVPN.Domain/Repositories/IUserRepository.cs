using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByLoginAsync(string login);
    }
}
