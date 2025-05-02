using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Configuration;
using System.Linq.Expressions;

namespace E_Commers_Project.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Category?>> GetAllCtgAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public new async Task AddAsync(Category category)
        {
            await _dbSet.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
