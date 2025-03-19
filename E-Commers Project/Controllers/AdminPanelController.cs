using E_Commers.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commers_Project.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AdminPanelController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Home()
        {
            var v = await _userRepository.GetAllAsync();
            return View(v);
        }
    }
}
