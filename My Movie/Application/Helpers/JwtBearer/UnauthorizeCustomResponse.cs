using My_Movie.Model;

namespace My_Movie.Application.Exceptions;

public static class UnauthorizeCustomResponse
{
    private static readonly ILogger Logger = LoggerFactory.Create(builder => { builder.AddConsole(); })
        .CreateLogger("UnauthorizedExceptionHandler");

    public static async Task HandleUnauthorizedAsync(HttpContext context)
    {
        var ApiResponse = new ApiResponse<object>
        {
            Code = StatusCodes.Status401Unauthorized,
            Status = "Unauthorized",
            Message = ["You are not authorized to access this resource"]
        };
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        Logger.LogWarning("Unauthorized access attempted.");

        await context.Response.WriteAsJsonAsync(ApiResponse);
    }
}