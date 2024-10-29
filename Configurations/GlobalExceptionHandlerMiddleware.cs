using GlobalExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = GlobalExceptionHandling.Exceptions.KeyNotFoundException;
using NotImplementedException = GlobalExceptionHandling.Exceptions.NotImplementedException;

namespace GlobalExceptionHandling.Configurations
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
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
                //Here in case if there is an execution failure (either an exception occurs in database, service or in the controller)
                //we have a try-catch here , we will be catching the error and we are passing that error into the HandleExceptionAsync() method
                //and we will be verifying what kind of error it is and we will be processing that and returning the response back to the user.

                await HandleExceptionAsync(context, ex);
            }
        
        }

        private static Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            HttpStatusCode statusCode;
            var stackTrace = string.Empty;
            string message = string.Empty;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException))
            {
                message=ex.Message;
                statusCode = HttpStatusCode.NotFound;
                stackTrace=ex.StackTrace;

            }
            else if (exceptionType == typeof(BadRequestException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;

            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;

            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;

            }
            else if (exceptionType == typeof(UnAuthorizedAccessException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.Unauthorized;
                stackTrace = ex.StackTrace;

            }
            else
            {
                message = ex.Message;
                statusCode = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exceptionResult);

        }
    }
}
