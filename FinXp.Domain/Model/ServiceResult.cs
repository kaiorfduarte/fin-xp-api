namespace FinXp.Domain.Model;

public readonly struct ServiceResult<T>
{
    public T Data { get; }
    public Exception Error { get; }
    public bool IsSuccess => Error == null;

    public ServiceResult(T data)
    {
        Data = data;
        Error = null;
    }

    public ServiceResult(Exception error)
    {
        Data = default;
        Error = error;
    }

    public static implicit operator ServiceResult<T>(T data) => new ServiceResult<T>(data);

    public static implicit operator ServiceResult<T>(Exception ex) => new ServiceResult<T>(ex);

}
