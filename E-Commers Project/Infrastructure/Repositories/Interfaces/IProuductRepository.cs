using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Infrastructure.Repositories.Interfaces
{
    public interface IProuductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        public Task<List<Product>> GetAllProductsWithImgAsync();
        //Task<List<Product>> GetTopRatedProductsAsync();
    }
    
    
}
