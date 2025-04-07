using MediatR;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Commands.POST.CreateFavouriteBookCommand;

public class CreateFavouriteBookCommandHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    ILogger<CreateFavouriteBookCommandHandler> logger)
    : IRequestHandler<Commands.CreateFavouriteBookCommand, ApiResponse<BookResponse>>
{
    public async Task<ApiResponse<BookResponse>> Handle(Commands.CreateFavouriteBookCommand command,
        CancellationToken cancellationToken)
    {
        var user = userRepository.GetAuthenticatedUser();
        var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
        var book = await bookRepository.GetBookByIdAsync(command.id);
        
         var new_FavouriteBook = new UserBooks
            {
                UserId = user_id,
                BookId = command.id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await bookRepository.AddFavouriteBook(new_FavouriteBook);
            logger.LogInformation("User with ID {UserId} has created the favourite book with ID {id}", user_id,
                command.id);

            return new ApiResponse<BookResponse>(200, "success", book);

    }
}