using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetCoreMVC.Filters.ActionFilter.Whitespace
{
    public class WhitespaceAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var response = context.HttpContext.Response;
            response.Body = new SpaceCleaner(response.Body);
        }
    }
}
