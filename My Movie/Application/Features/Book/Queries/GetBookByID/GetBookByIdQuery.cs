using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Queries.GetBookByID
{
    public record GetBookByIdQuery(int id) : IRequest<ApiResponse<BookResponse>>;

}
