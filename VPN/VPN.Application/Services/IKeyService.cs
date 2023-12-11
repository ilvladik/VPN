using SharedKernel.Dtos.Key;

namespace VPN.Application.Services
{
    public interface IKeyService : IDisposable
    {
        Task<KeyDtoResponse> CreateAsync();
        Task DeleteAsync(Guid id);
        Task<IEnumerable<KeyDtoResponse>> GetByServerIdAsync(Guid id);
        Task<IEnumerable<KeyDtoResponse>> GetAllAsync();
    }
}
