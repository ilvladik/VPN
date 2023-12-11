using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByLoginAsync(string login);
    }
}
