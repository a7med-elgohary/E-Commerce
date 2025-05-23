using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Commers_Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // حماية جميع endpoints للمسؤولين فقط
    public class AdminPanelController : Controller
    {
        private readonly IUserRepository _userRepository;
        private IUserService _userService;
        public AdminPanelController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Home")]
        public async Task<IActionResult> Home()
        {
            var userId = GetUserIdFromJwt();
            if (userId == null) return Unauthorized();

            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            var userId = GetUserIdFromJwt();
            if (userId == null) return Unauthorized();

            // يجب استبدال هذا بالبيانات الحقيقية من قاعدة البيانات
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 20 }
            };

            return Ok(products);
        }
        private int? GetUserIdFromJwt()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            if (tokenS == null) return null;

            var userId = tokenS.Claims.First(claim => claim.Type == "sub").Value;
            return int.Parse(userId);
        }
    }
}
