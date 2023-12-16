using ManagmentVPN.Domain.Entities;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagmentVPN.Infrastructure.Repositoies
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
            var server = await _context.Servers.FirstOrDefaultAsync(k => k.Id == id);
            if (server == null)
                return;
            _context.Servers.Remove(server);
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
