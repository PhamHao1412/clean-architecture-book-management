using MediatR;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Queries.GetAllBooks;

public class GetAllBooksQueryHandler(IBookRepository _bookRepository)
    : IRequestHandler<GetAllBooksQuery, ApiResponse<IEnumerable<BookResponse>>>
{
    public async Task<ApiResponse<IEnumerable<BookResponse>>> Handle(GetAllBooksQuery request,
        CancellationToken cancellationToken)
    {
        var booksList = await _bookRepository.GetAllBooks();
        return new ApiResponse<IEnumerable<BookResponse>>(200, "success", booksList);
    }
}