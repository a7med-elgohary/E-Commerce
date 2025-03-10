using Microsoft.OpenApi.Models;

namespace E_Commers.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // إضافة الخدمات
            builder.Services.AddControllers();

            // إضافة Swagger
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

            // تفعيل Swagger فقط في بيئة التطوير
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
                    options.RoutePrefix = "swagger"; // يجعل الوصول إلى Swagger عبر /swagger
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
