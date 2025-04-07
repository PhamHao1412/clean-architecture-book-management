using MediatR;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Commands
{
    public record DeleteBookCommand(int id) : IRequest<ApiResponse<Unit>>
    {
        // public int user_id { get; set; } // Property to store userId
    }
    
}
