using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace E_Commers_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ إضافة الخدمات
            builder.Services.AddControllersWithViews();

            // ✅ إضافة DbContext وربطه بـ SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ إضافة Dependency Injection
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // ✅ تفعيل CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            // ✅ تفعيل الـ Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
