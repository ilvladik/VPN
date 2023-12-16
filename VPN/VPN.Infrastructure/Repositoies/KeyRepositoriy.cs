using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VPN.Domain.Entities;
using VPN.Domain.Repositories;
using VPN.Infrastructure.Context;

namespace VPN.Infrastructure.Repositoies
{
    public class KeyRepositoriy : IKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public KeyRepositoriy(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Key key)
        {
            await _context.Keys.AddAsync(key);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Key? key = await _context.Keys.FirstOrDefaultAsync(x => x.Id == id);
            if (key != null)
                _context.Keys.Remove(key);
        }

        public async Task<IEnumerable<Key>> GetAllAsync()
        {
            return await _context.Keys.Include(k => k.Server).ToListAsync();
        }

        public async Task<Key?> GetByIdAsync(Guid id)
        {
            return await _context.Keys.Include(k => k.Server).FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<IEnumerable<Key>> GetByServerIdAsync(Guid serverId)
        {
            return await _context.Keys.Include(k => k.Server).Where(k => k.ServerId == serverId).ToListAsync();
        }
    }
}
