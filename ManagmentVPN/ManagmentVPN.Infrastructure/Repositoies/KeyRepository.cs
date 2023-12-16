using ManagmentVPN.Domain.Entities;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagmentVPN.Infrastructure.Repositoies
{
    public class KeyRepository : IKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public KeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Key key)
        {
            await _context.Keys.AddAsync(key);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var key = await _context.Keys.FirstOrDefaultAsync(k => k.Id == id);
            if (key == null)
                return;
            _context.Keys.Remove(key);
        }

        public async Task<Key?> GetByIdAsync(Guid id)
        {
            return await _context.Keys.FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<IEnumerable<Key>> GetAllAsync()
        {
            return await _context.Keys.ToListAsync();
        }

        public async Task UpdateAsync(Key key)
        {
            await Task.Run(() =>
            {
                _context.Keys.Update(key);
            });
        }
    }
}
