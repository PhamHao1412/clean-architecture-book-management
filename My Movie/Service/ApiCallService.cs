// namespace My_Movie.Service
// {
//     public class ApiCallService
//     {
//         private readonly HttpClient _httpClient;
//
//         public ApiCallService(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//         }
//
//         public async Task<string> CallApiAsync(string apiUrl)
//         {
//             var response = await _httpClient.GetAsync(GetApiUrl(apiUrl));
//             response.EnsureSuccessStatusCode();
//             var content = await response.Content.ReadAsStringAsync();
//             return content;
//         }
//         private string GetApiUrl(string apiName)
//         {
//             return apiName switch
//             {
//                 "GetAll" => "https://localhost:7220/books/GetAll",
//                 "GetBooksFromSoftwium" => "https://localhost:7220/books/GetBooksFromSoftwium",
//                 _ => null,
//             };
//         }
//     }
// }
