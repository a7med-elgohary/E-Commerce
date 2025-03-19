using E_Commers_Project.Models;
using System.Threading.Tasks;

namespace E_Commers.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindUserByEmailAsync(string email);
        Task<IEnumerable<User>?> GetActiveUsersAsync();
        Task ActivateUserAccount(int id);

    }
}
