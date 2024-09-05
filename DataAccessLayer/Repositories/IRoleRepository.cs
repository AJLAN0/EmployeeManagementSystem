using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleAsync(string name);
    }
}
