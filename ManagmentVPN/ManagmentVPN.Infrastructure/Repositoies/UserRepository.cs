using ManagmentVPN.Domain.Entities;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagmentVPN.Infrastructure.Repositoies
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstAsync(u => u.Login == login);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await Task.Run(() =>
            {
                _context.Users.Update(user);
            });
        }
    }
}
