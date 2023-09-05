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
        public async Task<User?> GetUserByIdAsync(string UserId)
        {
            var user = await _dbcontext.Users.FindAsync(UserId);
            return user;
        }
        public async Task<User?> GetUserByUsernameAsync(string Username)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == Username);
            return user;
        }
        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }
        public async Task<User?> UpdateUserByIdAsync(string UserId, User user)
        {
            var userData = await _dbcontext.Users.FindAsync(UserId);
            if(user.Name != null)
            {
                userData.Name = user.Name;
            }
            if(user.Password != null)
            {
                userData.Password = user.Password;
            }
            await _dbcontext.SaveChangesAsync();
            return userData;
        }
        public async Task<bool> DeleteUserByIdAsync(string UserId)
        {
            var user = await _dbcontext.Users.FindAsync(UserId);
            _dbcontext.Users.Remove(user);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
