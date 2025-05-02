using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commers_Project.Application.Services
{
    public class HomeScreenService : IHomeScreenService
    {
        private readonly ICategoryService _categoryService;

        public HomeScreenService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            

        }
        public async Task<HomeScreen> GetHomeScreenDataAsync()
        {
            var categories = await _categoryService.GetAllCtgAsync(); // استدعاء خدمة الـ Category

            return new HomeScreen(categories);

        }

       

    }
}
