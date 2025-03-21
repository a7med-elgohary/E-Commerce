using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
       

        public Task<bool> IsFounded(LoginModel model)
        {
            return _userRepository.IsFounded(model);
        }
        public async Task<User> CreateUserAsync(LoginModel model) {

           var theuser = await _userRepository.CreateUserAsync(new User { Email = model.Email, Name = model.UserName, Password = model.Password });
            return theuser;
        
        }

    }
}
