namespace InventoryManagmentSystem.Shared.Models;

public record EndPointResponse<T>(T Data, bool IsSuccess, string Message, bool IsAuthorized = true)
{
    public static EndPointResponse<T> Success(T data)
    {
        return new EndPointResponse<T>(data, true, string.Empty, true);
    }

    public static EndPointResponse<T> Success(T data, string message, bool isAuthorized = true)
    {
        return new EndPointResponse<T>(data, true, message, isAuthorized);
    }

    public static EndPointResponse<T> Failure(string message, bool isAuthorized = true)
    {
        return new EndPointResponse<T>(default, false, message, isAuthorized);
    }

    public static implicit operator EndPointResponse<T>(RequestResult<T> result)
    {
        if (result.IsSuccess)
            return Success(result.Data, result.Message);

        return Failure(result.Message);
    }
}
