using MediatR;
using My_Movie.Application.BookFeatures.Queries;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Queries.GetBookByID
{
    public class GetProductByIdQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBookByIdQuery, ApiResponse<BookResponse>>
    {
        public async Task<ApiResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
                var book = await bookRepository.GetBookByIdAsync(request.id);

                return new ApiResponse<BookResponse>(200,"success", book);

        }
    }
}
