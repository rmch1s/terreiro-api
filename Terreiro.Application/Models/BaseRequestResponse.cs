namespace Terreiro.Application.Models;

public class BaseRequestResponse<T>(T? data, bool error, string[] errorMessages)
{
    public T? Data { get; set; } = data;
    public bool Error { get; set; } = error;
    public string[] ErrorMessages { get; set; } = errorMessages;
}
