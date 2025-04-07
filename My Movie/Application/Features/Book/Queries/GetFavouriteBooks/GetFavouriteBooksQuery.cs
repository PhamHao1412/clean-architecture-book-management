using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Queries
{
    public record GetFavouriteBooksQuery : IRequest<ApiResponse<List<BookResponse>>>;
}

