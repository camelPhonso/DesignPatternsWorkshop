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
   
    /// <summary>
    /// Matcher method that reinforces handling of both possible outcomes of the Result pattern.
    /// The output final output will be wrapped in the unifying type passed to the method call.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="success"></param>
    /// <param name="failure"></param>
    public U Match<U>(Func<TResult, U> success, Func<TError, U> failure) => IsSuccess ? success(_value!) : failure(_error!);
}
