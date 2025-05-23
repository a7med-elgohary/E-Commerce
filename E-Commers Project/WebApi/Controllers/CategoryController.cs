using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Commers_Project.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<HomeController>  logger , ICategoryService categoryService )
        {
            _logger = logger;
            _categoryService = categoryService;

        }


        public IActionResult CategoryPage(int? id = null)
        {

            return View("CategoryPage", id);
        }

        // return 
        [HttpGet("GetAll")]
        public IActionResult GetAllCategory()
        {
            var userId = GetUserIdFromJwt();
            if (userId == null) return Unauthorized();

            var categories = _categoryService.GetAllCtgAsync();
            return Ok(categories);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateNew([FromForm] Category category)
        {
            var userId = GetUserIdFromJwt();
            if (userId == null) return Unauthorized();

            if (ModelState.IsValid) {
                try {
                    bool result = await _categoryService.addNewAsync(category);
                    if (result) {
                        _logger.LogInformation("Category with name {CategoryName} added successfully", category.Name);
                        return Ok();
                    }
                    else {
                        _logger.LogWarning("Failed to add category with name {CategoryName}", category.Name);
                        return BadRequest("Failed to create category");
                    }
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Error adding category with name {CategoryName}", category.Name);
                    return StatusCode(500, "An error occurred while creating the category");
                }
            }
            else {
                return BadRequest(ModelState);
            }
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
