using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace OniHealth.Web.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {

            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            var problemDetails = new ValidationProblemDetails()
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Detail = context.Exception.StackTrace,
                Title = "Internal Server Error"

            };

            problemDetails.Errors.Add("ServerError", new string[] { "Refresh the page and try again." });

            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;
        }
    }
}
