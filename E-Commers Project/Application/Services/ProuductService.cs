using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;

namespace E_Commers_Project.Application.Services
{
    public class ProuductService : IProuductService
    {
        private IProuductRepository _prouductRepository;
        public ProuductService(IProuductRepository prouductRepository)
        {
            _prouductRepository = prouductRepository;
        }
        public Task AddProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetNewArrivalsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetTopRatedProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
