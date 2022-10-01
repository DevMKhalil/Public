using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is null)
                return;

            var exception = context.Exception;

            //var errorResult = new { error = "An error occurred while processing your request" };

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request",
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
