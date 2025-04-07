using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Commands.DELETE.DeleteFavouriteBook;

public class DeleteFavouriteBookCommandHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    ILogger<DeleteFavouriteBookCommandHandler> logger)
    : IRequestHandler<DeleteFavouriteBookCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteFavouriteBookCommand command, CancellationToken cancellationToken)
    {
            var user = userRepository.GetAuthenticatedUser();
            var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
            await bookRepository.RemoveFavoriteBook(user_id,command.id);
            logger.LogInformation("User with ID {UserId} has deleted the favourite book with ID {id}", user_id,
                command.id);         
            return new ApiResponse<Unit>( default,"success");
    }
}
