using Microsoft.AspNetCore.Mvc;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.WebApi.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;

namespace E_Commers_Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProuductRepository _productRepository;

        public ProductController(IProuductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsWithImgAsync();
            return Ok(products);
        }
    }
}
