// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.Diagnostics;
using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bengbenz.Embassy.eServices.Infrastructure.Behaviors;

/// <summary>
/// Represents a MediatR pipeline behavior for logging request and response information.
/// This behavior logs the handling of each request, including its properties and the handling duration.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the request handler.</typeparam>
/// <remarks>
/// This class demonstrates the use of reflection to log request properties, which may impact performance.
/// It is recommended to evaluate the performance impact in scenarios where high throughput is required.
/// </remarks>
public sealed class CustomLoggingBehavior<TRequest, TResponse>(ILogger<Mediator> logger)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    // Guard.Against.Null(request);
    if (logger.IsEnabled(LogLevel.Information))
    {
      logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

      // Reflection! Could be a performance concern
      Type myType = request.GetType();
      IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
      foreach (PropertyInfo prop in props)
      {
        object? propValue = prop?.GetValue(request, null);
        logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
      }
    }

    var sw = Stopwatch.StartNew();

    var response = await next();

    logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms", typeof(TRequest).Name, response, sw.ElapsedMilliseconds);
    sw.Stop();
    return response;
  }
}
