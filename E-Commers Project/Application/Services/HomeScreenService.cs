using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commers_Project.Application.Services
{
    public class HomeScreenService : IHomeScreenService
    {
        private readonly ICategoryService _categoryService;
        private readonly ICartRepository _cartService;

        public HomeScreenService(ICategoryService categoryService , ICartRepository cartRepository)
        {
            _categoryService = categoryService;
            _cartService = cartRepository;
            

        }
        public async Task<HomeScreen> GetHomeScreenDataAsync(int? id = null)
        {
            
            var categories = await _categoryService.GetAllCtgAsync();
            Cart cart;
            if (id != null)
            {
                 cart = await _cartService.GetUserCart(id);
                return new HomeScreen(categories, cart);

            }
                return new HomeScreen(categories);
        }


    }
}
