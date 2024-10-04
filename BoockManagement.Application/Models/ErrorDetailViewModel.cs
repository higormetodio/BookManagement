using System.Text.Json;

namespace BookManagement.Application.Models;
public class ErrorDetailViewModel
{
    public ErrorDetailViewModel(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; private set; }
    public string Message { get; private set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
