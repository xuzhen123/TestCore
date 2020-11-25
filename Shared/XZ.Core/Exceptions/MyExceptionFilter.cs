
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace XZ.Core.Exceptions
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IKnownException knownException = context.Exception as IKnownException;
            if (knownException == null)
            {
                var logger = context.HttpContext.RequestServices.GetService<ILogger<MyExceptionFilter>>();
                logger.LogError(context.Exception, context.Exception.Message);
                knownException = KnownException.UnKown;
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }

            context.Result = new JsonResult(knownException)
            {
                ContentType = "application/json; charset=utf-8"
            };
        }
    }
}
