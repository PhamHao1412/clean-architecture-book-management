using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Commands.DELETE.DeleteBook;

public class DeleteBookHandlerCommand(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    ILogger<DeleteBookHandlerCommand> logger) :
    IRequestHandler<DeleteBookCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var user = userRepository.GetAuthenticatedUser();
        var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
        logger.LogInformation("User with ID {UserId} is trying to delete book with ID {BookId}", user_id, command.id);

        var book = await bookRepository.GetBookById(command.id);
        await bookRepository.DeleteBookAsync(book);
        logger.LogInformation("User with ID {UserId} has deleted book with ID {BookId}", user_id, command.id);

        return new ApiResponse<Unit>(200, "Success");
    }
}