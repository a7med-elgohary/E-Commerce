using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories.Interfaces;
using E_Commers_Project.Application.InterFaces;
using E_Commers_Project.Application.Services;
using E_Commers_Project.Infrastructure.Repositories;
using E_Commers_Project.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_Commers_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Seetion 
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthorization();

            // ✅ إضافة إعدادات JsonSerializer
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 options.JsonSerializerOptions.WriteIndented = true; // لو عايز JSON يكون منسق
             });


            // ✅ إضافة DbContext وربطه بـ SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ إضافة Dependency Injection
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IHomeScreenService, HomeScreenService>();
            builder.Services.AddScoped<IProuductRepository , ProuductRepository>();
            builder.Services.AddScoped<IProuductService , ProuductService>();

            // ✅ إضافة CORS (إذا كان مطلوبًا)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // ✅ إضافة Swagger للتوثيق (اختياري إذا كنت بحاجة له)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E-Commerce MVC API",
                    Version = "v1",
                    Description = "API Documentation for E-Commerce project"
                });
            });

            var app = builder.Build();

            // ✅ إعداد الـ Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                // ✅ تفعيل Swagger في بيئة التطوير فقط
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            // ✅ تفعيل CORS
            app.UseCors("AllowAll");

            // ✅ تفعيل الـ Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
