using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Application.Services;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Commers_Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        public CartItemRepository _cartItemRepository;
        private readonly IProuductRepository _productRepository;

        private ICartRepository _cartRepository;
        public CartController(IProuductRepository prouductService, ICartRepository cartRepository, CartItemRepository cartItemRepository)
        {
            _productRepository = prouductService;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }
        [HttpGet("GetCartItems/{userId}")]

        public async Task<IActionResult> GetCartItems(int userId)
        {
            var usercart = await _cartRepository.GetUserCart(userId);
            if (usercart == null)
                return NotFound("Cart not found");

            List<int>? userCartItemsid = await _cartItemRepository.GetCartItemByCartId(usercart.Id);
            if (userCartItemsid == null || !userCartItemsid.Any())
                return Json(new List<object>()); // رجع قائمة فارغة لو ما فيش منتجات

            var userCartItems = await _productRepository.GetProductById(userCartItemsid);
           
            return Json(userCartItems); // ترجع JSON للـ JavaScript
        }


        //RemoveProductFromCart
        [HttpDelete("RemoveProductFromCart/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var cart = await _cartRepository.GetUserCart(userId.Value);
            if (cart == null)
                return NotFound("Cart not found");

            var cartItems = await _cartItemRepository.GetCartItemByCartId2(cart.Id);
            var itemToRemove = cartItems.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove == null)
                return NotFound("Product not found in cart");

            await _cartItemRepository.DeleteAsync(itemToRemove.Id);
            return Ok("Product removed from cart");
        }



        [HttpGet("GetCartItems")]
        public async Task<IActionResult> GetCartItems()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                    return Unauthorized();

                var usercart = await _cartRepository.GetUserCart(userId.Value);
                if (usercart == null)
                    return Json(new List<object>());

                var userCartItemsid = await _cartItemRepository.GetCartItemByCartId(usercart.Id);
                if (userCartItemsid == null || !userCartItemsid.Any())
                    return Json(new List<object>());

                var userCartItems = await _productRepository.GetProductById(userCartItemsid);
                return Json(userCartItems);
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error in GetCartItems: {ex.Message}");
                return StatusCode(500, new { error = "An error occurred while retrieving cart items" });
            }
        }

        [HttpPost("AddProductToCart/{productId}")]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var cart = await _cartRepository.GetUserCart(userId.Value);
            if (cart == null)
            {
                // اختياري: لو الكارت مش موجود ممكن تعمله تلقائيًا
                return NotFound("Cart not found");
            }

            var newItem = new CartItem
            {
                ProductId = productId,
                CartId = cart.Id,
                Quantity = 1
            };

            await _cartRepository.AddCartItemAsync(userId.Value, newItem);
            return Ok();
        }

    }
}
