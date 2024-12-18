namespace Dima.Core.Messaging;

/// <summary>
/// Defines a handler for a request
/// </summary>
/// <typeparam name="TRequest">The request type being handled</typeparam>
/// <typeparam name="TResponse">The response type from the hnadler</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns>Response from the request</returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
