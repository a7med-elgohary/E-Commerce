using System.Diagnostics;
using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Application.Services;
using E_Commers_Project.Domain.Models;
using E_Commers_Project.Domain.ViewModels;
using E_Commers_Project.Infrastructure.Repositories;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Education.Classes.Item.Assignments.Item.Submissions.Item.Return;
using Microsoft.Graph.Models;

namespace E_Commers_Project.WebApi.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController>? _logger;
        private readonly IHomeScreenService? _homeScreen ;
        private readonly IProuductRepository _productRepository;
        private readonly CartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;


        public HomeController(IHomeScreenService homeScreenService, IProuductRepository productRepository, ICartRepository cartRepository , CartItemRepository cartItemRepository)
        {
            _homeScreen = homeScreenService;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;


        }
        // return all categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewmodel = await _homeScreen.GetHomeScreenDataAsync(null);
            return View(viewmodel);
        }
       
        public IActionResult ToAllProducts()
        {
            return View("AllProducts");
        }
        //public async Task<IActionResult> CheckOut()
        //{
        //    // الحصول على التوكن من الـ cookies
        //    var token = Request.Cookies["jwtToken"];

        //    //if (string.IsNullOrEmpty(token))
        //    //{
        //    //    // تخزين الرابط الحالي للعودة إليه بعد تسجيل الدخول
        //    //    HttpContext.Session.SetString("ReturnUrl", "/Home/CheckOut");
        //    //    // إعادة التوجيه إلى صفحة تسجيل الدخول
        //    //    return Redirect("/Auth/AuthScreen");
        //    //}
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null)
        //        return Unauthorized();

        //    var cart = await _cartRepository.GetUserCart(userId.Value);
        //    if (cart == null)
        //        return NotFound("Cart not found");
        //    var prouducts = await _cartItemRepository.GetCartItemByCartId2(cart.Id);
        //    return View("CheckOut", prouducts);
        //}


        public async Task<IActionResult> CheckOut()
        {
            // الحصول على التوكن من الـ cookies (اختياري)
            var token = Request.Cookies["jwtToken"];

            // التحقق من وجود UserId في الجلسة
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            //// جلب سلة المشتريات الخاصة بالمستخدم
            //var cart = await _cartRepository.GetUserCart(userId.Value);
            //if (cart == null)
            //    return NotFound("Cart not found");

            //// جلب عناصر السلة مع تحميل بيانات المنتج ضمن كل عنصر (تأكد من أن هذه الدالة تقوم بذلك)
            //var cartItems = await _cartItemRepository.GetCartItemByCartId2(cart.Id) ?? new List<CartItem>();

            //// التأكد أن كل عنصر يحتوي على بيانات المنتج لتحاشي NullReferenceException
            //foreach (var item in cartItems)
            //{
            //    if (item.Product == null)
            //    {
            //        // إذا كنت تستخدم EF Core، يمكنك إعادة تحميل المنتج هكذا:
            //        // item.Product = await _productRepository.GetProductById(item.ProductId);
            //        // أو تأكد أن الـ Include تم تطبيقه في الـ Repository.
            //    }
            //}
            var usercart = await _cartRepository.GetUserCart(userId);
            if (usercart == null)
                return NotFound("Cart not found");

            List<int>? userCartItemsid = await _cartItemRepository.GetCartItemByCartId(usercart.Id);
            if (userCartItemsid == null || !userCartItemsid.Any())
                return Json(new List<object>()); // رجع قائمة فارغة لو ما فيش منتجات

            var userCartItems = await _productRepository.GetProductById(userCartItemsid);

            // إرسال البيانات للعرض في View
            return View("CheckOut", userCartItems);
        }




        public IActionResult CategoryPage(int? id = null)
        {

            return View("CategoryPage",id);
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }
        public IActionResult about()
        {
            return View("about");
        }

        public async Task<IActionResult> Prouduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            // التحقق من أن المنتج ليس فارغًا
            if (product == null)
                return NotFound(); // إذا كان المنتج فارغًا، عرض صفحة Not Found

            // بناء الموديل
            ProuductPageViewModel model = new ProuductPageViewModel()
            {
                Nmae = product.Name,
                Descrption = product.Description,
                Price = product.Price,
                OldPrice =product.oldPrice,
                Stock = product.Stock,
                category = product.Category,
                // استخدام شرط للتأكد من أن المنتج يحتوي على تصنيف (category) قبل استخدامه
                RelatedProuducts = product.Category != null ? await _productRepository.GetProductsByCategoryIdAsync(product.Category.Id) : new List<Product>()
            };

            // إضافة الصور للموديل
            foreach (var item in product.Photos)
            {
                model.Photos.Add(item.Url);
            }

            // إرسال الموديل إلى الفيو
            return View("Prouduct", model);
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
