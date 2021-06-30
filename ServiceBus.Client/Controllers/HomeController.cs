using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBus.Client.Models;
using ServiceBus.Client.Infrastructure.Interfaces;
using ServiceBus.SharedLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBus.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailQueueBusService emailQueueBusService;

        public HomeController(ILogger<HomeController> logger, IEmailQueueBusService emailQueueBusService)
        {
            _logger = logger;
            this.emailQueueBusService = emailQueueBusService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Email email)
        {
            if (!ModelState.IsValid)
                return View(email);

            emailQueueBusService.SendEmailAsync(email);
           return View("Success");
        }
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
