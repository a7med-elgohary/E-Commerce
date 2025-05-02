using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Application.InterFaces
{
    public interface ICategoryService 
    {
        public Task<IEnumerable<Category?>> GetAllCtgAsync();
        public Task<bool> addNewAsync(Category category);
    }
}
