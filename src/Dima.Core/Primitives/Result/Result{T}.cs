namespace Dima.Core.Primitives.Result;

/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
/// <typeparam name="TValue">The result value type.</typeparam>
public class Result<TValue>
{
    private readonly TValue _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The result value.</param>
    public Result(TValue value)
        => _value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The result value.</param>
    /// <param name="status">The result status.</param>
    protected Result(TValue value, ResultStatus status)
        : this(value) => Status = status;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The result value.</param>
    /// <param name="status">The result status.</param>
    /// <param name="errors">The collection of errors.</param>
    protected Result(TValue value, ResultStatus status, IEnumerable<Error> errors)
        : this(value, status) => Errors = errors;

    /// <summary>
    /// Gets the collection of errors.
    /// </summary>
    public IEnumerable<Error> Errors { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.Created;

    /// <summary>
    /// Gets the status of the <see cref="Result"/>.
    /// </summary>
    public ResultStatus Status { get; } = ResultStatus.Ok;

    /// <summary>
    /// Gets the result value if the result is successful, otherwise throws an exception.
    /// </summary>
    /// <returns>The result value if the result is successful.</returns>
    /// <exception cref="InvalidOperationException">Throws when <see cref="Result.IsSuccess"/> is false.</exception>
    public TValue Value
        => IsSuccess
        ? _value
        : throw new InvalidOperationException("Cannot access value for a failure result.");

    /// <summary>
    /// Represents a successful result that occurred during the creation of a resource.
    /// </summary>
    /// <param name="value">The value of the resource created.</param>
    /// <returns>A <see cref="Result{TValue}"/> with status Created.</returns>
    public static Result<TValue> Created(TValue value)
        => new(value, ResultStatus.Created);

    /// <summary>
    /// Implicitly converts a value to a successful <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<TValue>(TValue value)
        => Success(value);

    /// <summary>
    /// Implicitly converts a <see cref="Result"/> to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    public static implicit operator Result<TValue>(Result result)
        => new(default!, result.Status, result.Errors);

    /// <summary>
    /// Represents an invalid result that occurred during the operation with a validation error.
    /// </summary>
    /// <param name="error">The validation <see cref="Error"/>.</param>
    /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error.</returns>
    public static Result<TValue> Invalid(Error error)
        => new(default!, ResultStatus.Invalid, [error]);

    /// <summary>
    /// Represents an invalid result that occurred during the operation with a list of validation errors.
    /// </summary>
    /// <param name="errors">The list of validation <see cref="Error"/>s.</param>
    /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified errors.</returns>
    public static Result<TValue> Invalid(IEnumerable<Error> errors)
        => new(default!, ResultStatus.Invalid, errors);

    /// <summary>
    /// Represents a successful result.
    /// </summary>
    /// <param name="value">The result value.</param>
    /// <returns>A <see cref="Result{TValue}"/> with status Ok.</returns>
    public static Result<TValue> Success(TValue value)
        => new(value, ResultStatus.Ok);
}
