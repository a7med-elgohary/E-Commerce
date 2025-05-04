using System.Diagnostics;
using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Application.Services;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Education.Classes.Item.Assignments.Item.Submissions.Item.Return;

namespace E_Commers_Project.WebApi.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController>? _logger;
        private readonly IHomeScreenService? _homeScreen ;
        public HomeController(IHomeScreenService homeScreenService)
        {
            _homeScreen = homeScreenService;

        }
        // return all categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewmodel = await _homeScreen.GetHomeScreenDataAsync();
            return View(viewmodel);
        }
        public IActionResult ToAllProducts()
        {
            return View("AllProducts");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
