using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.ActionFilters
{
    public class CourseApiExceptionActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && context.Exception is CourseApiException)
            {
                var exception = context.Exception as CourseApiException;
                if (exception.ErrorCode == ApiExceptionCodes.NotFound)
                {
                    context.ExceptionHandled = true;
                    context.Result = new NotFoundResult();
                }
                else if(exception.ErrorCode == ApiExceptionCodes.Conflict)
                {
                    context.ExceptionHandled = true;
                    context.Result = new ConflictResult();
                }
            }
        }
    }

    public class MyAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // before

            await next();

            // after 
        }
    }
}
