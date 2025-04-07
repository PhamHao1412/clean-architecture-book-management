using FluentValidation.Results;
using My_Movie.DTO;

namespace My_Movie.Model;
public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public T Data { get; set; }
    public List<string> Message { get; set; }

    public ApiResponse(int code, string status, T data)
    {
        Code = code;
        Status = status;
        Data = data;
    }

    public ApiResponse(int code, string status)
    {
        Code = code;
        Status = status;
    }
    public ApiResponse()
    {
        Message = new List<string>();
    }
    public static ApiResponse<T> Error(string message, int status = 500)
    {
        return new ApiResponse<T>(status, message, default);
    }

    public void AddValidationErrors(IEnumerable<ValidationFailure> errors)
    {
        if (errors == null) return;

        foreach (var failure in errors)
        {
            var errorMsg = $"Property {failure.PropertyName} failed validation. Error was {failure.ErrorMessage}";
            Message.Add(errorMsg);
        }
    }
}