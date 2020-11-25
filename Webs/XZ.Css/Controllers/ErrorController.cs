﻿
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using XZ.Core.Exceptions;

namespace XZ.Css.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = exceptionHandlerPathFeature?.Error;
            var knownException = ex as IKnownException;
            if (knownException == null)
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<ErrorController>>();
                logger.LogError(ex, ex.Message);
                knownException = KnownException.UnKown;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
            }

            return View(knownException);
        }
    }
}