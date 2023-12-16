using VPN.Application.OutlineApi.Entities;

namespace VPN.Application.OutlineApi
{
    public interface IOutlineProvider
    {
        Task<OutlineKey> CreateKeyAsync(OutlineServer server);
        Task DeleteKeyAsync(OutlineServer server, int outlineKeyId);
        Task<IEnumerable<OutlineKey>> GetAllKeysAsync(OutlineServer server);
        Task<OutlineKey> GetKeyByIdAsync(OutlineServer server, int outlineKeyId);
    }
}
