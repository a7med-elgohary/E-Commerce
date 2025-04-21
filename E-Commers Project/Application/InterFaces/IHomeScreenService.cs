using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;
using E_Commers_Project.WebApi.Controllers;

namespace E_Commers_Project.Application.InterFaces
{
    public interface IHomeScreenService
    {
        Task<HomeScreen> GetHomeScreenDataAsync();
    }
}
