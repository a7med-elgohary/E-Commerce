using E_Commers_Project.Domain.Models;
using System.Threading.Tasks;

namespace E_Commers.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>?> GetActiveUsersAsync();
        Task ActivateUserAccount(int id);
        Task<bool> IsFounded(LoginModel model);
        Task<User> CreateUserAsync(User user);
    }
}
