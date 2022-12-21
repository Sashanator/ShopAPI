using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ShopAPI.Features.Startup;

/// <inheritdoc />
public class IncludeDocumentFilter : IDocumentFilter

{
    private readonly Type[] _additionalTypes;

    /// <summary>
    /// </summary>
    public IncludeDocumentFilter()
    {
        var baseAssembly = Assembly.GetAssembly(typeof(Program));
        try
        {
            if (baseAssembly is { })
                _additionalTypes = baseAssembly.GetTypes()
                    .Where(x => x.GetTypeInfo().CustomAttributes
                        .Any(z => z.AttributeType == typeof(SwaggerIncludeAttribute)))
                    .ToArray();
        }
        catch
        {
        }
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (_additionalTypes != null)
            foreach (var t in _additionalTypes)
                context.SchemaGenerator.GenerateSchema(t, context.SchemaRepository);
    }
}

/// <summary>
///     Marks a class to be included as swagger document.
/// </summary>
public class SwaggerIncludeAttribute : Attribute
{
}