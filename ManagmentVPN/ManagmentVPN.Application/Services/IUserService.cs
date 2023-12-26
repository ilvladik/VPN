using ManagmentVPN.Domain.Entities;

namespace ManagmentVPN.Application.Services
{
    public interface IUserService : IDisposable
    {
        Task AllowAsync(Guid id);
        Task ForbidAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
