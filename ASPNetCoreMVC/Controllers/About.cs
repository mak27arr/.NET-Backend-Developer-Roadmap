﻿using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreMVC.Controllers
{
    [Route("{controller}/{action}")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
