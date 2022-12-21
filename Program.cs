using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddApiVersioning();
        services
            .AddShopServiceDbContext()
            .AddShopServiceEntityFrameworkRepositories();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(o =>
        {
            
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMicroservice();

        app.MapControllers();

        app.Run();
    }
}