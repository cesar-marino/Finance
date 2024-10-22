using Finance.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Filters
{
    public class ApiGlobalExceptionFilter(IHostEnvironment env) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();
            var exception = context.Exception;

            if (env.IsDevelopment())
                details.Extensions.Add("StackTrace", exception.StackTrace);

            if (exception is NotFoundException notFoundException)
            {
                details.Title = notFoundException?.Code;
                details.Status = StatusCodes.Status404NotFound;
                details.Type = notFoundException?.GetType().ToString();
                details.Detail = notFoundException?.Message;
            }
            else if (exception is UnexpectedException unexpectedException)
            {
                details.Title = unexpectedException?.Code;
                details.Status = StatusCodes.Status500InternalServerError;
                details.Type = unexpectedException?.GetType().ToString();
                details.Detail = unexpectedException?.Message;
            }

            context.HttpContext.Response.StatusCode = (int)details.Status!;
            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
