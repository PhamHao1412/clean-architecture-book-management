using MediatR;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Queries.GetBooksFromApiThirdParty;

public record GetBooksFormApiThirdPartyQuery : IRequest<ApiResponse<IEnumerable<Model.Book>>>;
