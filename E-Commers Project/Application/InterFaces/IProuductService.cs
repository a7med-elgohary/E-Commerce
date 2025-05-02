using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Application.InterFaces
{
    public interface IProuductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<List<Product>> SearchProductsAsync(string searchTerm);
        Task<List<Product>> GetTopRatedProductsAsync();
        Task<List<Product>> GetNewArrivalsAsync();
    }
}
