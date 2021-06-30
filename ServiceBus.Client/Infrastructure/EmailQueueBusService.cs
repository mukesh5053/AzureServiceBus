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
        public IConfiguration _config{ get; }

        public EmailQueueBusService(IConfiguration configuration)
        {
            _config = configuration;
        }
       
        public async Task SendEmailAsync(Email email)
        {
            //Get connection string and Queue name from the appsetting
            string connection = _config.GetValue<string>("AzureServiceBus:AzureServiceBusConnection");
            string queueName = _config.GetValue<string>("AzureServiceBus:QueueName").ToString();

            //Initializing/connecting to Queue
            var queueClient = new QueueClient(connection, queueName);

            //convert object into json and initialize the queue message
            var msg = new Message(Encoding.UTF8.GetBytes( JsonSerializer.Serialize(email)));

            await queueClient.SendAsync(msg);

        }
    }
}
