using ASPNetCoreMVC.Filters.ActionFilter.Whitespace;
using ASPNetCoreMVC.Filters.ExceptionFilter;
using ASPNetCoreMVC.Filters.ResourceFilter;
using ASPNetCoreMVC.Filters.ResultFilter;
using ASPNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNetCoreMVC.Controllers
{
    [CustomExceptionFilter]
    [SimpleResourceFilter]
    [Route("{controller}/{action}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [DateTimeExecutionFilter]
        [Whitespace]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}