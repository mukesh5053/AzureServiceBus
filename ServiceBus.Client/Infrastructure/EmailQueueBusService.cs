using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using ServiceBus.Client.Infrastructure.Interfaces;
using ServiceBus.Client.Infrastructure.Services;
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
        public IMessagePublisher _messagePublisher { get; }

        public EmailQueueBusService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }


        public async Task SendEmailAsync(Email email)
        {
            try
            {
                await _messagePublisher.Queue(email);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
