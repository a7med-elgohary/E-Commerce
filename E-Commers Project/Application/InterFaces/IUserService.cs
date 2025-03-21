using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Application.InterFaces
{
    public interface IUserService 
    {
       Task<User?> GetUserByEmailAsync(string email);
       Task<bool> IsFounded(LoginModel model);
        Task<User> CreateUserAsync(LoginModel model);

    }
}
