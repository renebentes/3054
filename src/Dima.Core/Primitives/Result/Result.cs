namespace Dima.Core.Primitives.Result;

/// <summary>
/// Represents a result of operations with status information and possibly errors.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    protected Result()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with a specified status and a collection of errors.
    /// </summary>
    /// <param name="status">The <see cref="ResultStatus"/></param>
    /// <param name="errors">The <see cref="Failure"/> collection</param>
    protected Result(ResultStatus status, IEnumerable<Error> errors)
    {
        Status = status;
        Errors = errors;
    }

    /// <summary>
    /// Gets the collection of errors
    /// </summary>
    public IEnumerable<Error> Errors { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the result is a success result.
    /// </summary>
    public bool IsSuccess => Status is ResultStatus.Ok;

    /// <summary>
    /// Gets the status of the <see cref="Result"/>
    /// </summary>
    public ResultStatus Status { get; } = ResultStatus.Ok;

    /// <summary>
    /// Represents a failure that occurred during the operation with an error
    /// </summary>
    /// <param name="error">The <see cref="Error"/></param>
    /// <returns>A new instance of <see cref="Result"/> with the specified error</returns>
    public static Result Failure(Error error)
        => new(ResultStatus.Failure, [error]);

    /// <summary>
    /// Represents a failure that occurred during the operation with a list of errors.
    /// </summary>
    /// <param name="errors">The list of <see cref="Error"/>s</param>
    /// <returns>A new instance of <see cref="Result"/> with the list of errors.</returns>
    public static Result Failure(IEnumerable<Error> errors)
        => new(ResultStatus.Failure, errors);

    /// <summary>
    /// Represents a successful operation without return type
    /// </summary>
    /// <returns>A <see cref="Result"/></returns>
    public static Result Success()
        => new();
}
