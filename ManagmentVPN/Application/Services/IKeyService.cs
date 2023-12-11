using SharedKernel.Dtos.Key;

namespace Application.Services
{
    public interface IKeyService : IDisposable
    {
        Task<IEnumerable<KeyDtoResponse>> GetAllAsync();
    }
}
