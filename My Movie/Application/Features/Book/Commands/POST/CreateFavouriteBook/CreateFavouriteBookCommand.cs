using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Commands
{
    public record CreateFavouriteBookCommand(int id) : IRequest<ApiResponse<BookResponse>>;
}