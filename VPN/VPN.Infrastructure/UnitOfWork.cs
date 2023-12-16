using VPN.Domain;
using VPN.Domain.Repositories;
using VPN.Infrastructure.Context;
using VPN.Infrastructure.Repositoies;

namespace VPN.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Keys = new KeyRepositoriy(_context);
            Servers = new ServerRepositoriy(_context);
        }
        public IServerRepository Servers { get; private set; }

        public IKeyRepository Keys { get; private set; }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
