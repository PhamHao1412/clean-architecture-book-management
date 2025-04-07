using FluentValidation.Results;
using My_Movie.Model;

namespace My_Movie.DTO
{
    public class ApiPageResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Error { get; set; } = new();
        public PageList<T> Items { get; set; } = new();


        public ApiPageResponse(int statusCode, string message, PageList<T> items)
        {
            StatusCode = statusCode;
            Message = message;
            Items = items;
        }

        public ApiPageResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiPageResponse()
        {
            Error = new List<string>();
        }
        public void AddValidationErrors(IEnumerable<ValidationFailure> errors)
        {
            if (errors == null) return;

            foreach (var failure in errors)
            {
                var errorMsg = $"Property {failure.PropertyName} failed validation. Error was {failure.ErrorMessage}";
                Error.Add(errorMsg);
            }
        }
    }
}