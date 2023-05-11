namespace Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        try
        {
            return _next(httpContext);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}