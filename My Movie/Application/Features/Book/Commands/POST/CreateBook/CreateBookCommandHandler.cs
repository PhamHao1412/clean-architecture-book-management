using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Commands.POST.CreateBook;

public class CreateBookCommandHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository
   , ILogger<CreateBookCommandHandler> logger
    )
    : IRequestHandler<CreateBookCommand, ApiResponse<BookResponse>>
{

    public async Task<ApiResponse<BookResponse>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var user = userRepository.GetAuthenticatedUser();
        var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);

        logger.LogInformation("User with ID {UserId} is creating a book", user_id);

        var newBook = new Model.Book
        {
            title = command.title,
            isbn = command.isbn,
            pageCount = command.pageCount,
            Authors = command.Authors,
            creatAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        };
        var response = await bookRepository.AddBookAsync(newBook);
        logger.LogInformation("User with ID {UserId} has created a book.", user_id);

        return new ApiResponse<BookResponse>(200, "success", response);
    }
}