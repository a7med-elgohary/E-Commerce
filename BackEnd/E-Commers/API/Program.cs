using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace E_Commers.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ إضافة الخدمات (Dependency Injection)
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // ✅ إضافة DbContext وربطه بـ SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ✅ إضافة CORS للسماح للـ Frontend بالتواصل مع API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // ✅ إضافة الخدمات الأساسية
            builder.Services.AddControllers();

            // ✅ إضافة Swagger للتوثيق
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E-Commerce API",
                    Version = "v1",
                    Description = "API Documentation for E-Commerce project"
                });
            });

            var app = builder.Build();

            // ✅ إعداد Swagger في بيئة التطوير فقط
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
                    options.RoutePrefix = "swagger"; // يجعل الوصول إلى Swagger عبر /swagger
                });
            }

            // ✅ تفعيل CORS
            app.UseCors("AllowAll");

            // ✅ تفعيل HTTPS
            app.UseHttpsRedirection();

            // ✅ تفعيل Authorization
            app.UseAuthorization();

            // ✅ تشغيل التطبيق
            app.MapControllers();
            app.Run();
        }
    }
}
