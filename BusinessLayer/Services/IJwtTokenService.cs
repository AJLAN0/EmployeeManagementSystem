using DataAccessLayer.Models;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
