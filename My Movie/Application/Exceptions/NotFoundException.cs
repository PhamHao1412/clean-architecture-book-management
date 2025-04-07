using System.Net;
using My_Movie.Model;

namespace My_Movie.Application.Exceptions;

public class NotFoundException : BaseException
        
{
        public NotFoundException(string message) : base(message, "Not Found", HttpStatusCode.NotFound)
        {
        }
        public NotFoundException(string message, int id) : base(message, "Not Found", HttpStatusCode.NotFound)
        {
        }
        public NotFoundException(string message, string TermSearchTerm) : base(message, "Not Found", HttpStatusCode.NotFound)
        {
        }
}
