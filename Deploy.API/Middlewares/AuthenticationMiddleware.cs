namespace Deploy.API.Middlewares;

public class AuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();
        const string tokenScheme = "Bearer ";
        
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith(tokenScheme, StringComparison.OrdinalIgnoreCase))
        {
            var token = authorizationHeader[tokenScheme.Length..].Trim();
            if (token == Environment.GetEnvironmentVariable("ACCESS_TOKEN"))
            {
                await next(context);
                return;
            }
        }
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Authorization header missing or invalid.");
    }
}