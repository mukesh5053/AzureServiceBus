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
    class PaymentEmailTopicBusService : IPaymentEmailTopicBusService
    {
        public IMessagePublisher _messagePublisher { get; }

        public PaymentEmailTopicBusService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }


        public async Task SendEmailAsync(PaymentEmail email)
        {
            try
            {
              await _messagePublisher.PublishTopics(email);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task SendReminderEmailAsync(PaymentReminderEmail email)
        {
            try
            {
                await _messagePublisher.PublishTopics(email);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
