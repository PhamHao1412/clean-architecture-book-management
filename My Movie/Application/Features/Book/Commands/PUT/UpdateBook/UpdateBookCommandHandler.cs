using System.Security.Claims;
using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.CommandHandlers;

public class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    ILogger<UpdateBookCommandHandler> logger)
    : IRequestHandler<UpdateBookCommand, ApiResponse<BookResponse>>
{
    public async Task<ApiResponse<BookResponse>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
       
            var user = userRepository.GetAuthenticatedUser();
            var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
            logger.LogInformation("User with ID {UserId} is updating the book with book ID {id}", user_id, command.id);

            var updateBook = await bookRepository.GetBookById(command.id);
            
            updateBook.title = command.title;
            updateBook.isbn = command.isbn;
            updateBook.pageCount = command.pageCount;
            updateBook.Authors = command.Authors;
            updateBook.updatedAt = DateTime.UtcNow;
            var response = await bookRepository.UpdateBookAsync(updateBook);
            
            logger.LogInformation("User with ID {UserId} has updated the book with book ID {id}", user_id, command.id);
            return new ApiResponse<BookResponse>(200, "success", response);
    }
}