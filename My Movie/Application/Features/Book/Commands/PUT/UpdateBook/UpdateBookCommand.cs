using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Commands
{
    public record UpdateBookCommand(int id, string title, string isbn, int pageCount, List<string> Authors) : IRequest<ApiResponse<BookResponse>>;

}
