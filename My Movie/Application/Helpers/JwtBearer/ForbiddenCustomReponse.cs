using My_Movie.Model;

namespace My_Movie.Application.Exceptions;

public abstract class ForbiddenCustomReponse
{
    private static readonly ILogger logger = LoggerFactory.Create(builder => { builder.AddConsole(); })
        .CreateLogger("UnauthorizedExceptionHandler");
    public static async Task HandleForbiddenAsync(HttpContext context)
    {
        var ApiResponse = new ApiResponse<object>
        {
            Code = StatusCodes.Status403Forbidden,
            Status = "Forbidden",
            Message = ["You do not have permission to access this resource."]
        };
        logger.LogWarning("Forbidden access attempted.");
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsJsonAsync(ApiResponse);
    }
}