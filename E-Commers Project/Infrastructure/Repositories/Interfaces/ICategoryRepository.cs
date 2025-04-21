using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Infrastructure.Repositories.Interfaces
{

    public interface ICategoryRepository : IRepository<Category>
    {
       public Task<IEnumerable<Category?>> GetAllCtgAsync();
    }
}
