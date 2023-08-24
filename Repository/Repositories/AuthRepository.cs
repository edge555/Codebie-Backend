using Database;
using Database.Models;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public AuthRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<User> PostUserAsync(User user)
        {
            var newUser = _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return newUser.Entity;
        }
    }
}
