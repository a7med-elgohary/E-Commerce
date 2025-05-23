using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Domain.Models;
using NuGet.Protocol.Core.Types;

namespace E_Commers_Project.Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Task<Cart> GetUserCart(int? id);
        public Task AddCartItemAsync(int userId,CartItem cartItem);
    }
}
