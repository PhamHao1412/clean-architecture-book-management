using MediatR;
using My_Movie.Application.BookFeatures.Queries;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.QueryHandlers;

public class GetAllBooksQueryCacheHandler(IBookRepository _bookRepository)
    : IRequestHandler<GetAllBooksQueryCache, ApiResponse<IEnumerable<BookResponse>>>
{
    public async Task<ApiResponse<IEnumerable<BookResponse>>> Handle(GetAllBooksQueryCache request,
        CancellationToken cancellationToken)
    {
        var booksList = await _bookRepository.GetAllBooksCache();
        return new ApiResponse<IEnumerable<BookResponse>>(200, "success", booksList);
    }
}