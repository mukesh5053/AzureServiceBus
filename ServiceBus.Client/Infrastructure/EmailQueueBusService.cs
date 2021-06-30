using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using ServiceBus.Client.Infrastructure.Interfaces;
using ServiceBus.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Client.Infrastructure
{
    class EmailQueueBusService : IEmailQueueBusService
    {
        public IQueueClient _queueClient { get; }

        public EmailQueueBusService(IQueueClient queueClient)
        {
            this._queueClient = queueClient;
        }
       
        public async Task SendEmailAsync(Email email)
        {
            try
            {
                //convert object into json and initialize the queue message
                var msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(email)));

                await _queueClient.SendAsync(msg);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
