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
    public class ValidationActionFilter : IAsyncActionFilter
    {
        private readonly CourseApiExceptionActionFilter _courseApiExceptionActionFilter;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var productToValid = context.ActionArguments.FirstOrDefault(x => x.Value is ProductDto);
            if (productToValid.Value  == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await next();
        }

        public ValidationActionFilter(CourseApiExceptionActionFilter courseApiExceptionActionFilter)
        {
            _courseApiExceptionActionFilter = courseApiExceptionActionFilter;
        }
    }
}
