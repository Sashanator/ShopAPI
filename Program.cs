using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Models;
using ShopAPI.Features;
using ShopAPI.Features.DataAccess;
using ShopAPI.Features.Startup;

namespace ShopAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        // Add services to the container.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddControllers();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();
        services
            .AddShopServiceDbContext()
            .AddInternalServiceCollection()
            .AddShopServiceEntityFrameworkRepositories();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ssorin Shop API",
                Version = "v1",
                Description = "It's shop API for my subject Software Design"
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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