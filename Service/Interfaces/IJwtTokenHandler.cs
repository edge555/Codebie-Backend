using Database.Models;

namespace Service.Interfaces
{
    public interface IJwtTokenHandler
    {
        string GenerateJwtToken(User user);
        string GetLoggedInUserId();
        Boolean IsTokenExpired();
        void DeleteToken();
    }
}
