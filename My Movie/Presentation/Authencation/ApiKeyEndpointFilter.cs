using MediatR;
using My_Movie.Model;

namespace My_Movie.Presentation.Authencation;

public class ApiKeyEndpointFilter(IConfiguration configuration) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApikeyHeaderName, out var extractedApiKey))
            return new ApiResponse<Unit>(401, "API Key missing");

        var api_key = configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        if (api_key != null && !api_key.Equals(extractedApiKey)) return new ApiResponse<Unit>(401, "Invalid API Key");
        return await next(context);
    }
}