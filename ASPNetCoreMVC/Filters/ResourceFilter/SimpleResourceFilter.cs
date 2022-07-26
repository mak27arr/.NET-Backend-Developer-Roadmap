using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetCoreMVC.Filters.ResourceFilter
{
    public class SimpleResourceFilter : Attribute, IResourceFilter, IOrderedFilter
    {
        public int Order => int.MaxValue;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Response.Headers.IsReadOnly)
                context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy HH-mm-ss"));
        }
    }
}
