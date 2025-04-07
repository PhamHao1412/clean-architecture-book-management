using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using My_Movie.Application.Exceptions;
using My_Movie.Model;

namespace My_Movie.Presentation.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var responseError = new ApiResponse<object>();
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            responseError.Code = (int)HttpStatusCode.BadRequest;
            responseError.Status = "Validation errors";

            if (validationException.Errors != null)
                responseError.AddValidationErrors(validationException.Errors);
        }
        else if (exception is BaseException baseException)
        {
            httpContext.Response.StatusCode = (int)baseException.StatusCode;
            responseError.Code = (int)baseException.StatusCode;
            responseError.Status = baseException.Error;
            responseError.Message = [baseException.Message];
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            responseError.Code = (int)HttpStatusCode.InternalServerError;
            responseError.Status = "Internal Server Error.";
            responseError.Message = [exception.Message];
        }
        logger.LogError("{responseError}", responseError.Status);
        await httpContext.Response.WriteAsJsonAsync(responseError, cancellationToken).ConfigureAwait(false);
        return true;
    }
}