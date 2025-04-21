using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;

namespace E_Commers_Project.Domain.ViewModels
{
    public class HomeScreen
    {
        // قائمة الفئات (Categories)
        public IEnumerable<Category?> Categories { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        // هذا هو المُنشئ الذي يتم فيه تهيئة الكائنات
        public HomeScreen(IEnumerable<Category?> categories)
        {
            Categories = categories;
        }
    }
}
