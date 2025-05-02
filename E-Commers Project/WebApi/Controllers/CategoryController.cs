using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        // return 
        public IActionResult GetAllCategory()
        {
            var categories = _categoryService.GetAllCtgAsync();

            ViewBag["categories"] = categories;
            return View("Index");
        }

        public async Task<IActionResult> CreateNew([FromForm] Category category)
        {
            if (ModelState.IsValid) {

                try{

                    bool result = await _categoryService.addNewAsync(category);
                    if (result)
                    {
                        _logger.LogInformation("categroy with name {CategoryName} added successfully" , category.Name);
                        return RedirectToAction(/* return to the Addnew category bage */);
                    }
                    else
                    {
                        _logger.LogWarning("failed to add category with name {CategoryName}", category.Name);
                        //add error massger to user
                        ModelState.AddModelError("", "create failed");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error category with name {CategoryName}", category.Name);
                    // you can add error message to user in model state with random error
                }
            }
            else
            {
                // return add new category page with the smae wrong data add massage to user
                //add in key inputbox name
                ModelState.AddModelError("","Invalid data");

            }

            return View(category);

        }


    }
}
