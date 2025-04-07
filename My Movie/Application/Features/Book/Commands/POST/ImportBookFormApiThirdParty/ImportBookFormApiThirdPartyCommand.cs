using MediatR;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Commands.POST.ImportBookFormApiThirdParty;

public class ImportBookFormApiThirdPartyCommand :  IRequest<ApiResponse<IEnumerable<Model.Book>>>
{
    
}