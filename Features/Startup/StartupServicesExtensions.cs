using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ShopAPI.Features.Startup;

/// <summary>
///     Extensions for Startup
/// </summary>
public static class StartupServicesExtensions
{
    private static string XmlCommentsFilePath
    {
        get
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }
    }

    /// <summary>
    ///     Add swagger config
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                //options.OperationFilter<SwaggerDefaultValues>();
                // add custom headers fields
                //options.OperationFilter<SwaggerHeaderParametersFilter>();
                // integrate xml comments
                options.DocumentFilter<IncludeDocumentFilter>();

                options.IncludeXmlComments(XmlCommentsFilePath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
    }

    /// <summary>
    ///     Redirect root document queries to Swagger UI page.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    private static async Task RedirectToSwagger(HttpContext context, Func<Task> next)
    {
        if (context.Request.Path.Value == "/")
        {
            context.Response.Redirect($"/swagger{context.Request.QueryString}");
            return;
        }

        await next();
    }

    /// <summary>
    ///     Basic microservice middleware
    /// </summary>
    /// <param name="app"></param>
    public static void UseMicroservice(this IApplicationBuilder app)
    {
        app.UseStaticFiles();
        app.UseCors("AllowAll");

        app.UseRouting();
        

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Values}/{action=Get}/{id?}");
        });
    }

    /// <summary>
    ///     Swagger middleware extension
    /// </summary>
    /// <param name="app"></param>
    /// <param name="provider"></param>
    /// <param name="configuration"></param>
    public static void UseSwaggerCustom(this IApplicationBuilder app, IApiVersionDescriptionProvider provider,
        IConfiguration configuration)
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url =
                            $"https://{httpReq.Host.Value}{configuration.GetValue<string>("PathBaseSettings:ApplicationPathBase")}"
                    }
                };
            });
        });
        app.UseSwaggerUI(
            options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint(
                        $"{configuration.GetValue<string>("PathBaseSettings:ApplicationPathBase")}/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
            });

        app.Use(RedirectToSwagger);
    }
}