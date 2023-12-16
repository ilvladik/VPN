using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Application.Services
{
    public interface IUserService : IDisposable
    {
        Task RegisterAsync(string login,  string password);
        Task<User> LoginAsync(string login, string password);
        Task AllowAsync(Guid id);
        Task Forbid(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
