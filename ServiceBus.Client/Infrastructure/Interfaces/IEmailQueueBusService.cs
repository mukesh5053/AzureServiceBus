using ServiceBus.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBus.Client.Infrastructure.Interfaces
{
    public interface IEmailQueueBusService
    {
        Task SendEmailAsync(Email email);
    }
}
