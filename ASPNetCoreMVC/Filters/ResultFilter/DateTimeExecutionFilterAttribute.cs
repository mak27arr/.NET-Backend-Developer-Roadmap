using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetCoreMVC.Filters.ResultFilter
{
    public class DateTimeExecutionFilterAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("DateTime", DateTime.Now.ToString());
            await next();
        }
    }
}
