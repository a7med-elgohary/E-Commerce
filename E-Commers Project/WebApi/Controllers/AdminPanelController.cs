using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commers_Project.WebApi.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IUserRepository _userRepository;
        private IUserService _userService;
        public AdminPanelController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Home()
        {
            var v = await _userRepository.GetAllAsync();
            string? userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;
            return View(v);
        }

        public async Task<IActionResult> Product()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 20 },
                new Product { Id = 2,
                Name = "Mechanical Keyboard",
                Description = "RGB mechanical keyboard with blue switches.",
                Price = 79.50m,
                Stock = 80,
                CreateAt = DateTime.UtcNow.AddDays(-20),
                IsFavorite = false,
                CategoryId = 1,
                SaleId = 2
                },

            };

            return View(products);
        }
    }
}
