

using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositoies
{
    public class ServerRepository : IServerRepository
    {
        private readonly ApplicationDbContext _context;
        public ServerRepository(ApplicationDbContext context)
        { 
            _context = context; 
        }

        public async Task AddAsync(Server server)
        {
            await _context.Servers.AddAsync(server);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _context.Servers.Remove(await _context.Servers.FirstAsync(s => s.Id == id));
        }

        public async Task<Server?> GetByIdAsync(Guid id)
        {
            return await _context.Servers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _context.Servers.ToListAsync();
        }

        public async Task UpdateAsync(Server server)
        {
            await Task.Run(() =>
            {
                _context.Servers.Update(server);
            });
        }
    }
}
