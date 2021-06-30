using Microsoft.Azure.ServiceBus;
using ServiceBus.Client.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Client.Infrastructure.Services
{
    public class MessagePublisher : IMessagePublisher
    {
        public string _azureServiceBusConnection { get; }
        public string _queueName { get; }
        public string _topicName { get; }

        public MessagePublisher(string azureServiceBusConnection, string queueName, string topicName)
        {
            _azureServiceBusConnection = azureServiceBusConnection;
            _queueName = queueName;
            _topicName = topicName;
        }


        public async Task Queue<T>(T obj)
        {
            try
            {
               var _queueClient = new QueueClient(_azureServiceBusConnection, _queueName);
                //convert object into json and initialize the queue message
                var msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj)));

                await _queueClient.SendAsync(msg);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task PublishTopics<T>(T obj)
        {
            try
            {
                var _topicClient = new TopicClient(_azureServiceBusConnection, _topicName);
                //convert object into json and initialize the queue message
                var msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj)));
                msg.UserProperties["messageType"] = typeof(T).Name;
                await _topicClient.SendAsync(msg);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
