using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Common.Api;

public static class ExceptionMiddleware
{
    public static async Task HandleException<T>(Exception exception, HttpContext context)
    {
        //var innerException = GetInnerException(exception);
        var statusCode = GetStatusCode(exception.Demystify());

        var logger = context.RequestServices.GetRequiredService<ILogger<T>>();
        logger.LogError(exception, "Exception middleware caught an exception");

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Exception = exception.ToString(),
                ReadFriendlyException = exception.Demystify().ToString()
            };
            await context.Response.WriteAsync(response.ToString());
        }
    }

    private static Exception GetInnerException(Exception exception)
    {
        if (exception.InnerException == null)
        {
            return exception;
        }

        return GetInnerException(exception.InnerException);
    }

    private static HttpStatusCode GetStatusCode(Exception exception)
    {
        return exception is ArgumentException ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError;
    }
}
