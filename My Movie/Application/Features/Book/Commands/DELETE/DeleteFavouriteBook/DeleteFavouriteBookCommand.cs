using MediatR;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Commands
{
    public record DeleteFavouriteBookCommand(int id) : IRequest<ApiResponse<Unit>>;


}
