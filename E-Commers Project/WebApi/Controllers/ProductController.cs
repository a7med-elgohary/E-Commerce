using Microsoft.AspNetCore.Mvc;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.WebApi.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace E_Commers_Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProuductRepository _productRepository;
        private readonly IConfiguration _configuration;

        public ProductController(IProuductRepository productRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _configuration = configuration;
        }

        [HttpGet("Getall")]
        [AllowAnonymous] // السماح للجميع بالوصول
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsWithImgAsync();
            return Ok(products);
        }

        private int? GetUserIdFromJwt()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) 
                return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                if (tokenS == null) 
                    return null;

                var userIdClaim = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return null;

                return userId;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("GetProductsById/{id}")]
        [AllowAnonymous] // السماح للجميع بالوصول
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
                
            return Ok(product);
        }


        [HttpGet("GetCategoryProuducts/{id}")]
        [AllowAnonymous] // السماح للجميع بالوصول
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategoryProductsByID(int id)
        {
            var products = await _productRepository.GetAllProductsWithImgAsync();
            var result = products.Where(p => p.CategoryId == id);
            return Ok(result);
        }

    }
}
