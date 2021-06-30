using Microsoft.Azure.ServiceBus;
using ServiceBus.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBus.Client.Infrastructure.Interfaces
{
    public interface IPaymentEmailTopicBusService 
    {
        Task SendEmailAsync(PaymentEmail email);

        Task SendReminderEmailAsync(PaymentReminderEmail email);
    }
}
