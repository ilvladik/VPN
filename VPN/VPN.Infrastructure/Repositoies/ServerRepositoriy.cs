using System.Text.Json;
using VPN.Domain.Repositories;
using VPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VPN.Infrastructure.Context;

namespace VPN.Infrastructure.Repositoies
{
    public class ServerRepositoriy : IServerRepository
    {
        private readonly ApplicationDbContext _context;
        public ServerRepositoriy(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Server server)
        {
            await _context.AddAsync(server);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Server? server = await _context.Servers.Include(s => s.Keys).FirstOrDefaultAsync(x => x.Id == id);
            if (server != null)
                _context.Servers.Remove(server);
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _context.Servers.Include(s => s.Keys).ToListAsync();
        }

        public async Task<Server?> GetByIdAsync(Guid id)
        {
            return await _context.Servers.Include(s => s.Keys).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
