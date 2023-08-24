using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public UserRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbcontext.Users.OrderBy(x => x.Username).ToListAsync();
        }
        public async Task<User> GetUserByGuIdAsync(string GuId)
        {
            var user = await _dbcontext.Users.FindAsync(GuId);
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string Username)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == Username);
            return user;
        }
        public async Task<User> GetUserByEmailAsync(string Email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }
        
    }
}
