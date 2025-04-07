using MediatR;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;

namespace My_Movie.Application.Features.Book.Queries.GetBooksQuery;

public class GetBooksQueryHandler(IBookRepository _bookRepository)
    : IRequestHandler<BookFeatures.Queries.GetBooksQuery, ApiPageResponse<BookResponse>>
{
    public async Task<ApiPageResponse<BookResponse>> Handle(BookFeatures.Queries.GetBooksQuery query,
        CancellationToken cancellationToken)
    {
            var results = await _bookRepository.GetProductsQuery(query.SearchTerm,
                query.SortColumn,
                query.SortOrder,
                query.Page,
                query.PageSize);
            if (results == null || results.TotalCount == 0) throw new NotFoundException("$The books with key term{query.SearchTerm} was not found",query.SearchTerm);
            return new ApiPageResponse<BookResponse>(200, "success", results);
    }
}