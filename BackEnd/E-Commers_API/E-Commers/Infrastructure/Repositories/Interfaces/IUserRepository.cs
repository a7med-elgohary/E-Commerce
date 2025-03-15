using E_Commers.Domain.Entities;
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
