using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace E_Commers_Project.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Cart> GetUserCart(int? id) => await _dbSet.FirstOrDefaultAsync(c => c.UserId == id);
        public async Task AddCartItemAsync(int userId, CartItem cartItem)
        {
            var cart = await GetUserCart(userId);
            if (cart != null)
            {
                cart.CartItems ??= new List<CartItem>();
                cart.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
            }
        }
        //public async Task CreateCartForUser(int userId)
        //{
        //    // تحقق إذا كان لدى المستخدم عربة بالفعل
        //    var existingCart = await _dbSet.FirstAsync(c => c.UserId == userId);

        //    if (existingCart == null)
        //    {
        //        // أنشئ عربة جديدة
        //        var newCart = new Cart
        //        {
        //            UserId = userId
        //        };

        //        _context.Carts.Add(newCart);
        //        await _context.SaveChangesAsync();
        //    }
        //}


    }
}
