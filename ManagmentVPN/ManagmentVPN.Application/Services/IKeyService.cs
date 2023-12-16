using ManagmentVPN.Application.Dtos;

namespace ManagmentVPN.Application.Services
{
    public interface IKeyService : IDisposable
    {
        Task<KeyDto> AddAsync(Guid userId);
        Task DeleteAsync(Guid id);
        Task<KeyDto> GetByIdAsync(Guid id);
        Task<KeyVpnDto> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<KeyDto>> GetAllAsync();
    }
}
