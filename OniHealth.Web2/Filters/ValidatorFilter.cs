using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using OniHealth.Web.Config;
using OniHealth.Domain.Enums;

namespace OniHealth.Web.Filters
{
    public class ValidatorFilter : IAsyncResultFilter
    {

        private readonly IValidator _validator;

        public ValidatorFilter(IValidator validator)
        {
            _validator = validator;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_validator.Return == StatusCodeReturn.NotFound)
            {
                var problemDetalhes = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Detail = "NotFoundException",
                    Title = "Not Found"
                };

                context.Result = new NotFoundObjectResult(problemDetalhes);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (_validator.Return == StatusCodeReturn.BadRequest)
            {
                var problemDetalhes = new ValidationProblemDetails
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "View the error properties to see more details.",
                    Title = "Validator error"
                };

                problemDetalhes.Errors.Add("Validator", _validator.Messages);

                context.HttpContext.Response.ContentType = "application/json";

                context.Result = new BadRequestObjectResult(problemDetalhes);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await next();
        }
    }
}
