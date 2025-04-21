using Microsoft.AspNetCore.Mvc;
using E_Commers_Project.Domain.Models;  // تأكد من تضمين النموذج المناسب
using System.Collections.Generic;      // لتستخدم List أو أي نوع بيانات

namespace E_Commers_Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // مثال على كيفية الحصول على جميع المنتجات
        // يمكن استبدال هذه البيانات بأي خدمة أو قاعدة بيانات
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 20 },
                new Product { Id = 2,
        Name = "Mechanical Keyboard",
        Description = "RGB mechanical keyboard with blue switches.",
        Price = 79.50m,
        Stock = 80,
        CreateAt = DateTime.UtcNow.AddDays(-20),
        IsFavorite = false,
        CategoryId = 1,
        SaleId = 2

    },

            };

            return Ok(products);  // إرجاع البيانات بصيغة JSON
        }
    }
}
