using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq.Expressions;

namespace E_Commers_Project.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Category?>> GetAllCtgAsync()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
