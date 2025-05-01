namespace DesignPatternsWorkshop.Domain.Result;

public readonly struct Result<TResult, TError>
{
    private readonly TError? _error;
    
    private readonly TResult? _value;
    
    public bool IsSuccess { get; }

    private Result(TResult value)
    {
        IsSuccess = true;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    public static implicit operator Result<TResult, TError>(TResult value) => new(value);

    public static implicit operator Result<TResult, TError>(TError error) => new(error);
    
    public U Match<U>(Func<TResult, U> success, Func<TError, U> failure) => IsSuccess ? success(_value!) : failure(_error!);
}
