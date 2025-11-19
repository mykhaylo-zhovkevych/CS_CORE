using ConsoleApp5._4Remastered.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryAPI.Filters
{
    public class HttpExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var (status, title) = context.Exception switch
            {
                IsAlreadyBorrowedException => (StatusCodes.Status409Conflict, "Item is already borrowed"),
                IsAlreadyReservedException => (StatusCodes.Status409Conflict, "Item is already reserved"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid request"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Not found"),
                InvalidOperationException => (StatusCodes.Status409Conflict, "Invalid operation"),
                _ => (StatusCodes.Status500InternalServerError, "Unexpected error")
            };

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = context.Exception.Message
            };

            // serialize ProblemDetails Object into JSON
            // Sets the result so the client receives a proper response
            context.Result = new ObjectResult(problem) { StatusCode = status };
            context.ExceptionHandled = true;
        }
    }
}