using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure.Context;
using ManagmentVPN.Infrastructure.Repositoies;

namespace ManagmentVPN.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Servers = new ServerRepository(_context);
            Keys = new KeyRepository(_context);
            Users = new UserRepository(_context);
        }
        public IServerRepository Servers { get; private set; }

        public IKeyRepository Keys { get; private set; }

        public IUserRepository Users { get; private set; }


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
