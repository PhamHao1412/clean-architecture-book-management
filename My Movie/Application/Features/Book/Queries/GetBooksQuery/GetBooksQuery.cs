using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.BookFeatures.Queries;

public record GetBooksQuery(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<ApiPageResponse<BookResponse>> ;
