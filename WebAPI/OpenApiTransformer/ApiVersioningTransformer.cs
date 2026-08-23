using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace WebAPI.OpenApiTransformer
{
    public class ApiVersioningTransformer(ApiVersionDescription description) : IOpenApiDocumentTransformer
    {
        public ApiVersionDescription Description { get; } = description;

        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            document.Info = new()
            {
                Title = $"WebAPI",
                Version = Description.ApiVersion.ToString(),
                Description = $"WebAPI for Polls {(Description.IsDeprecated ? "This api is deprecated." : string.Empty)}"
            };

            return Task.CompletedTask;
        }
    }
}
