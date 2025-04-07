using System.Net;

namespace My_Movie.Application.Exceptions;

public class UnauthorizedException: BaseException
{
    public UnauthorizedException(string message) : base(message, "Unauthorized", HttpStatusCode.Unauthorized)
    {
    }
    public UnauthorizedException(string message, string name) : base(message, "Unauthorized", HttpStatusCode.Unauthorized)
    {
    }
}