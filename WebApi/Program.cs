using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TuyaApp.Application.Services;
using TuyaApp.Domain.Interfaces;
using TuyaApp.Infrastructure.Persistence;

namespace TuyaApp.WebApi
{
    public partial class Program { }

    public class Startup
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "WebApi Tuya - Prueba Técnica",
                    Version = "v1.2",
                    Description = "Desarrollado por Santiago Naranjo Sanchez"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                var applicationXml = Path.Combine(AppContext.BaseDirectory, "TuyaApp.Application.xml");
                options.IncludeXmlComments(applicationXml);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
