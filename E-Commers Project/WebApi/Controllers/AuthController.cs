using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Application.Services;
using E_Commers_Project.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace E_Commers_Project.WebApi.Controllers
{
 
    public class AuthController  : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration  , IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
            
        }

        public IActionResult AuthScreen()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("genral", "Invalid email or password.");
                return View("AuthScreen", model);
            }

            bool isFounded = await _userService.IsFounded(model);
            if (!isFounded)
            {
                ModelState.AddModelError("genral", "Email not founded sign up first.");
                return View("AuthScreen", model);
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);
            //  حفظ بيانات المستخدم في الجلسة
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);
            
            string? name= HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = name;
            if (user.IsAdmin) 
            {
                return RedirectToAction("Home", "AdminPanel");
            }
            else
            {
                return RedirectToAction("Index", "Home"); 
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(LoginModel model)
        {
            // modlestate errors
            var errorMessages = new Dictionary<string, string>
            {
                { "Email", "Email not valid" },
                { "Password", "Passowrd not valid" },
                { "UserName", "User name not valid " }
                
            };
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    // ✅ إذا كان الحقل موجودًا في القاموس، نعرض رسالته
                    if (errorMessages.ContainsKey(key))
                    {
                        ModelState.AddModelError(key, errorMessages[key]);
                    }
                    else
                    {
                        // ✅ إذا لم يكن الحقل موجودًا، نستخدم رسالة عامة
                        ModelState.AddModelError(key, "هناك خطأ في الإدخال، يرجى التحقق.");
                    }
                }
            }
            var Isfounded = await _userService.IsFounded(model);
            if (Isfounded)
            {
                ModelState.AddModelError("Genral", "account already created");
            }

            var theuser =  await _userService.CreateUserAsync(model);
            HttpContext.Session.SetString("UserEmail", theuser.Email);
            HttpContext.Session.SetInt32("UserRoel", theuser.IsAdmin ? 1 :0 );
            HttpContext.Session.SetString("UserName", theuser.Name);
            HttpContext.Session.SetInt32("UserId", theuser.Id);
           
            return RedirectToAction("Index","Home",model);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") // ✅ تحويل الـ bool إلى نص مناسب
        // ✅ إضافة دور المستخدم إذا كان لديك أدوار
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
