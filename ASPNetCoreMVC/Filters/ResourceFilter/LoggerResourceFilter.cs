using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetCoreMVC.Filters.ResourceFilter
{
    public class LoggerResourceFilter : Attribute, IResourceFilter, IOrderedFilter
    {
        ILogger _logger;
        public int Order => int.MinValue;

        public LoggerResourceFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("SimpleResourceFilter");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation($"Executed - {DateTime.Now} {context.HttpContext.Request.Path}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation($"Executing - {DateTime.Now} {context.HttpContext.Request.Path}");
        }
    }
}
