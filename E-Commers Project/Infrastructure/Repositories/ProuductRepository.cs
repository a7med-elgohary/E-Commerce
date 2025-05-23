using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commers_Project.Infrastructure.Repositories
{
    public class ProuductRepository : GenericRepository<Product>, IProuductRepository
    {
        public ProuductRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            // This method retrieves all products that belong to a specific category.

            return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();

        }

        public async Task<List<Product>> GetAllProductsWithImgAsync()
        {
            return await _dbSet.Include(p => p.Photos).ToListAsync();
        }
        public override async Task<Product?> GetByIdAsync(int id)
        {

            Product? result = await _dbSet.Include(c => c.Photos)
                .Include(e=>e.Category)
                .FirstOrDefaultAsync(c => c.Id == id);
                


            return result ;
        }
        //public async Task<List<Product>> GetTopRatedProductsAsync()
        //{
        //    //return await _dbSet
        //    //    .Where(p => p.Ratin >= 4.5) // Assuming rating is a property of Product
        //    //    .OrderByDescending(p => p.Rating)
        //    //    .ToListAsync();
        //}

        public async Task<List<Product>?> GetProductById(List<int>? ids)
        {
            var data = await _dbSet
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.Photos)
                .ToListAsync();

            return data;
        }

    }
}
