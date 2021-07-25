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
    public class AddCreatedByActionFilter<T>: IAsyncActionFilter where T :  ICreatedBy
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parameter = context.ActionArguments.FirstOrDefault(x => x.Value is T);
            var itemToFill =  parameter.Value as ICreatedBy;
            var copyObj = itemToFill with { CreatedBy = "NameFromAction " + Guid.NewGuid() };
            context.ActionArguments[parameter.Key] = copyObj;
            var executedContext = await next();
            //executedContext.Result // result after action invoked.
        }
    }
}
