using E_Commers_Project.Application.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commers_Project.WebApi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<HomeController>  logger , ICategoryService categoryService )
        {
            _logger = logger;
            _categoryService = categoryService;

        }

        public IActionResult GetAllCategory()
        {
            var categories = _categoryService.GetAllCtgAsync();

            ViewBag["categories"] = categories;
            return View("Index");
        }


    }
}
