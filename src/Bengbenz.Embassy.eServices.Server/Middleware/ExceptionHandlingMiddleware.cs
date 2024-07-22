// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using System.Net;
using Bengbenz.Embassy.eServices.Core;
using Bengbenz.Embassy.eServices.Core.Exceptions;

namespace Bengbenz.Embassy.eServices.Server.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await next(httpContext);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(httpContext, ex);        
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";

    if (exception is DuplicateException duplicationException)
    {
      context.Response.StatusCode = (int)HttpStatusCode.Conflict;
      await context.Response.WriteAsync(new ErrorDetails(
        context.Response.StatusCode, duplicationException.Message).ToString());
    }
    else
    {
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await context.Response.WriteAsync(new ErrorDetails(
        context.Response.StatusCode, exception.Message).ToString());
    }
  }
}
