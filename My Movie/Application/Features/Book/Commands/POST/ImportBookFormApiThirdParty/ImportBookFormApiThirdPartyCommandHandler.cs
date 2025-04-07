using System.Text.Json;
using MediatR;
using My_Movie.Application.Features.Book.Commands.POST.ImportBookFormApiThirdParty;
using My_Movie.Application.Features.Book.Queries.GetBooksFromApiThirdParty;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;
using Polly;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace My_Movie.Application.Features.Book.Commands.POST.ImportBookFormSoftwium;

public class ImportBookFormApiThirdPartyCommandHandler(HttpClient httpClient, IBookRepository _bookRepository,
    IAsyncPolicy<HttpResponseMessage> retryPolicy,
    ILogger<GetBooksFormApiThirdPartyQueryHandler> logger) :  IRequestHandler<ImportBookFormApiThirdPartyCommand, ApiResponse<IEnumerable<Model.Book>>>
{
    public async Task<ApiResponse<IEnumerable<Model.Book>>> Handle(ImportBookFormApiThirdPartyCommand request, CancellationToken cancellationToken)
    {
        var response = await retryPolicy.ExecuteAsync(() =>
            httpClient.GetAsync("https://softwium.com/api/books", cancellationToken));
        response.EnsureSuccessStatusCode();
        logger.LogInformation("Fetching books from third-party API...");
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var books = JsonSerializer.Deserialize<IEnumerable<Model.Book>>(responseBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var enumerable = books!.ToList();
        if (enumerable.Count == 0)
            return new ApiResponse<IEnumerable<Model.Book>>(404, "not found", enumerable);

        var booksList = await _bookRepository.GetAllBooks();
        if (booksList.Count == 0)
            foreach (var book in enumerable)
            {
                if (book?.isbn == null) book!.isbn = "";
                var newBook = new Model.Book
                {
                    title = book.title,
                    isbn = book.isbn,
                    pageCount = book.pageCount,
                    Authors = book.Authors,
                    creatAt = book.creatAt,
                    updatedAt = book.updatedAt
                };
                await _bookRepository.AddBookAsync(newBook);
            }

        logger.LogInformation("Fetched books from third-party API successfully.");
        return new ApiResponse<IEnumerable<Model.Book>>(200, "success", enumerable);
        
    }
}