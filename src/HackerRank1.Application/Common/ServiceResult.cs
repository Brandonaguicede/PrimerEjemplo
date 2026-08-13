namespace HackerRank1.Application.Common;

public enum ServiceResultStatus
{
    Success,
    NotFound,
    ValidationFailed,
    Unauthorized
}

public class ServiceResult
{
    protected ServiceResult(ServiceResultStatus status, string? error = null)
    {
        Status = status;
        Error = error;
    }

    public ServiceResultStatus Status { get; }

    public string? Error { get; }

    public bool Succeeded => Status == ServiceResultStatus.Success;

    public static ServiceResult Success() => new(ServiceResultStatus.Success);

    public static ServiceResult NotFound(string? error = null) => new(ServiceResultStatus.NotFound, error);

    public static ServiceResult ValidationFailed(string? error = null) => new(ServiceResultStatus.ValidationFailed, error);

    public static ServiceResult Unauthorized(string? error = null) => new(ServiceResultStatus.Unauthorized, error);
}

public sealed class ServiceResult<T> : ServiceResult
{
    private ServiceResult(ServiceResultStatus status, T? value = default, string? error = null)
        : base(status, error)
    {
        Value = value;
    }

    public T? Value { get; }

    public static ServiceResult<T> Success(T value) => new(ServiceResultStatus.Success, value);

    public static new ServiceResult<T> NotFound(string? error = null) => new(ServiceResultStatus.NotFound, error: error);

    public static new ServiceResult<T> ValidationFailed(string? error = null) => new(ServiceResultStatus.ValidationFailed, error: error);

    public static new ServiceResult<T> Unauthorized(string? error = null) => new(ServiceResultStatus.Unauthorized, error: error);
}
