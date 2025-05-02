using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace E_Commers_Project.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Category?>> GetAllCtgAsync()
        {
            return await _categoryRepository.GetAllCtgAsync();
        }
        public async Task<bool> addNewAsync(Category category)
        {      
               await _categoryRepository.AddAsync(category);
               return true;
        }


    }
}
