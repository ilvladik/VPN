using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositoies
{
    public class KeyRepository : IKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public KeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Key key)
        {
            await _context.Keys.AddAsync(key);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _context.Keys.Remove(await _context.Keys.FirstAsync(k => k.Id == id));
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
