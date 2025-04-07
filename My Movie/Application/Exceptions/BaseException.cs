using System.Net;

namespace My_Movie.Application.Exceptions;

public class BaseException : System.Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Error { get; }
    public BaseException(string message, string error ,HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        Error = error;
        StatusCode = statusCode;
    }
}