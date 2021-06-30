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
    public class TopicController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentEmailTopicBusService _paymentEmailTopicBusService;

        public TopicController(ILogger<HomeController> logger, IPaymentEmailTopicBusService paymentEmailTopicBusService)
        {
            _logger = logger;
            this._paymentEmailTopicBusService = paymentEmailTopicBusService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Topic()
        {
            return View();
        }


        public IActionResult TopicReminder()
        {
            return View("Topic");
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Topic(PaymentEmail email)
        {
            if (!ModelState.IsValid)
                return View(email);

            _paymentEmailTopicBusService.SendEmailAsync(email);
           return View("~/Views/home/Success.cshtml");
        }


        [HttpPost]
        public IActionResult TopicReminder(PaymentEmail email)
        {
            if (!ModelState.IsValid)
                return View(email);

            _paymentEmailTopicBusService.SendReminderEmailAsync(new PaymentReminderEmail(email.EmailTo, email.Amount, email.Message));
            return View("~/Views/home/Success.cshtml");
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
