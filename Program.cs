using System.Reflection;
using MediatR;
using ShopAPI.Features.DataAccess;
using ShopAPI.Features.Startup;

namespace ShopAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddControllers();
        builder.Services
            .AddShopServiceDbContext()
            .AddShopServiceEntityFrameworkRepositories();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerConfiguration();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMicroservice();

        app.MapControllers();

        app.Run();
    }
}