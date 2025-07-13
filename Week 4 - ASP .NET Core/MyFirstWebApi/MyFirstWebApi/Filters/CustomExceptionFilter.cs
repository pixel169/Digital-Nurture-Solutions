using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFirstWebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            File.AppendAllText("exceptions.txt", $"[{DateTime.Now}] {exception.Message}\n");

            context.Result = new ObjectResult("An unexpected error occurred")
            {
                StatusCode = 500
            };
        }
    }
}
