using System.Text.Json;
using MediatR;
using My_Movie.Model;
using Polly;

namespace My_Movie.Application.Features.Book.Queries.GetBooksFromApiThirdParty;

public class GetBooksFormApiThirdPartyQueryHandler(
    HttpClient httpClient,
    IAsyncPolicy<HttpResponseMessage> retryPolicy,
    ILogger<GetBooksFormApiThirdPartyQueryHandler> logger)
    : IRequestHandler<GetBooksFormApiThirdPartyQuery, ApiResponse<IEnumerable<Model.Book>>>
{
    public async Task<ApiResponse<IEnumerable<Model.Book>>> Handle(GetBooksFormApiThirdPartyQuery request, CancellationToken cancellationToken)
    {
        var response = await retryPolicy.ExecuteAsync(() => httpClient.GetAsync("https://softwium.com/api/books", cancellationToken));
        response.EnsureSuccessStatusCode();
    
        logger.LogInformation("Fetching books from third-party API...");
    
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var books = JsonSerializer.Deserialize<IEnumerable<Model.Book>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    
        var enumerable = books!.ToList();
        if (enumerable.Count == 0)
        {
            logger.LogWarning("No books found from third-party API.");
            return new ApiResponse<IEnumerable<Model.Book>>(404, "not found", enumerable);
        }
    
        logger.LogInformation("Fetched books from third-party API successfully.");
        return new ApiResponse<IEnumerable<Model.Book>>(200, "success", enumerable);
    }
}