﻿using System.Net;
using System.Text.Json;

namespace Tmf.Ecom.Api.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message;
        ErrorMessage errorMessage = new ErrorMessage();

        var exceptionType = exception.GetType();

        //if (exceptionType == typeof(BadRequestException))
        //{
        //    message = exception.Message;
        //    status = HttpStatusCode.BadRequest;
        //    stackTrace = exception.StackTrace;
        //}
        //else if (exceptionType == typeof(NotFoundException))
        //{
        //    message = exception.Message;
        //    status = HttpStatusCode.NotFound;
        //    stackTrace = exception.StackTrace;
        //}
        if (exceptionType == typeof(HttpRequestException))
        {
            status = HttpStatusCode.InternalServerError;
            errorMessage.Message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            status = HttpStatusCode.NotImplemented;
            errorMessage.Message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            status = HttpStatusCode.Unauthorized;
            errorMessage.Message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(KeyNotFoundException))
        {
            status = HttpStatusCode.Unauthorized;
            errorMessage.Message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            errorMessage.Message = exception.Message;
            stackTrace = exception.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = errorMessage.Message, stackTrace });
        //var exceptionResult = JsonSerializer.Serialize(errorMessage);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
