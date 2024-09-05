using DataAccessLayer.Configuration;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.DataAccessRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context
                             , ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

        }

        public IQueryable<User> GetAllUsersAsQueryable()
        {
            return _context.Users
                .AsQueryable();
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            _logger.LogInformation("Fetching user by email: {Email}", email);

            var user = await _context.Users.Include(t => t.Role)
                                           .SingleOrDefaultAsync(t => t.Email == email);
            if (user == null)
            {
                _logger.LogWarning("No user found with email: {Email}", email);
            }
            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
