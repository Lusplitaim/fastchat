using FastChat.Core.Exceptions;

namespace FastChat.ExceptionHandlers
{
    public class ExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is CoreException)
                {
                    context.Response.StatusCode = GetStatusCode(ex, context);
                }
            }
        }

        private int GetStatusCode(Exception ex, HttpContext httpContext) => ex switch
        {
            NotFoundCoreException => StatusCodes.Status404NotFound,
            ForbiddenCoreException => StatusCodes.Status403Forbidden,
            _ => httpContext.Response.StatusCode,
        };
    }
}
