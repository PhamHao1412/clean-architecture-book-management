using System.Security.Claims;
using MediatR;
using My_Movie.Application.BookFeatures.Queries;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.QueryHandlers;

public class GetFavouriteBooksQueryHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    ILogger<GetFavouriteBooksQueryHandler> logger)
    : IRequestHandler<GetFavouriteBooksQuery, ApiResponse<List<BookResponse>>>
{
    public async Task<ApiResponse<List<BookResponse>>> Handle(GetFavouriteBooksQuery query,
        CancellationToken cancellationToken)
    {
       var user = userRepository.GetAuthenticatedUser();
            
            var user_id =int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
            var favoriteBooks = await bookRepository.GetFavoriteBooks(user_id);
         

            return new ApiResponse<List<BookResponse>>(200,"success", favoriteBooks);
        
        
    }
}