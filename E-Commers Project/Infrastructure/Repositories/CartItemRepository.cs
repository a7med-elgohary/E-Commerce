using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commers_Project.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }

        // returns a list of product ids in the cart
        public async Task<List<int>> GetCartItemByCartId(int id)
        {
            var productIds = await _dbSet
                .Where(x => x.CartId == id)
                .Select(x => x.ProductId)
                .Where(pid => pid.HasValue)         // تجاهل القيم null
                .Select(pid => pid.Value)           // استخراج القيمة الفعلية
                .ToListAsync();

            return productIds;
        }

        // returns a list of cart items by cart id
        public async Task<List<CartItem>> GetCartItemByCartId2(int cartId)
        {
            return await _dbSet
                .Where(x => x.CartId == cartId)
                .ToListAsync();
        }




    }
}
