using System.Net;

namespace My_Movie.Application.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message, "Bad Request", HttpStatusCode.BadRequest)
    {
    }
    public BadRequestException(string message, string name) : base(message, "Bad Request", HttpStatusCode.BadRequest)
    {
    }
}