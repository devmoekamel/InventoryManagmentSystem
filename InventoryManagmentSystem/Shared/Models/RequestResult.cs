namespace InventoryManagmentSystem.Shared.Models;

public record RequestResult<T>(T Data, bool IsSuccess, string Message)
{
    public static RequestResult<T> Success(T data, string message)
    {
        return new RequestResult<T>(data, true, message);
    }

    public static RequestResult<T> Success(T data)
    {
        return new RequestResult<T>(data, true, string.Empty);
    }
    public static RequestResult<T> Success()
    {
        return new RequestResult<T>(default, true, string.Empty);
    }

    public static RequestResult<T> Failure(string message)
    {
        return new RequestResult<T>(default, false, message);
    }
}
