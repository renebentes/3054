namespace Dima.Core.Messaging;

/// <summary>
/// Represents a request with a response.
/// </summary>
/// <typeparam name="TResponse">A response type</typeparam>
public interface IRequest<out TResponse>;
