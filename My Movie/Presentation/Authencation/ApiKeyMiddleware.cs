// using MediatR;
// using My_Movie.Model;
//
// namespace My_Movie.Presentation.Authencation;
//
// public class ApiKeyMiddleware : IMiddleware
// {
//     private readonly IConfiguration _configuration;
//
//     public ApiKeyMiddleware(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//
//   //Using CustomMiddleware
//     public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//     {
//         if (!context.Request.Headers.TryGetValue(AuthConstants.ApikeyHeaderName, out var extractedApiKey) &&
//             context.GetEndpoint()?.Metadata.GetMetadata<RequireApiKeyAttributes>() is not null)
//         {
//             var response = new TResponse<Unit>(401, "API key missing");
//             await context.Response.WriteAsJsonAsync(response);
//             return ;
//         }
//         var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
//         if (!apiKey.Equals(extractedApiKey))
//         {
//             var response = new TResponse<Unit>(401, "Invalid API key");
//             await context.Response.WriteAsJsonAsync(response);
//             return;
//         }
//         await next.Invoke(context);
//     }
// }