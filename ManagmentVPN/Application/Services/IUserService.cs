using Application.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(UserDto userDto);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByLoginAsync(string login);
        Task AllowVpnAccess(Guid id); 
        Task DisableVpnAccess(Guid id);
        Task<KeyDtoForUserResponse?> GetVpn(Guid id);
    }
}
