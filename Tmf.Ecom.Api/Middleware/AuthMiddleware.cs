using System.Text.Json;

namespace Tmf.Ecom.Api.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        ErrorMessage errorMessage = new ErrorMessage();
        context.Request.Headers.TryGetValue("BpNo", out var bpNo);
        context.Request.Headers.TryGetValue("UserType", out var userType);

        //do the checking
        if (string.IsNullOrEmpty(Convert.ToString(bpNo)) || string.IsNullOrEmpty(Convert.ToString(bpNo)))
        {
            errorMessage.Message = "Please add BpNo in request header.";
            var exceptionResult = JsonSerializer.Serialize(new { error = errorMessage.Message });
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(exceptionResult);
            return;
        }

        if (string.IsNullOrEmpty(Convert.ToString(userType)) || string.IsNullOrEmpty(Convert.ToString(userType)))
        {
            errorMessage.Message = "Please add UserType in request header.";
            var exceptionResult = JsonSerializer.Serialize(new { error = errorMessage.Message });
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(exceptionResult);
            return;
        }
        await _next(context);
       
    }

}
