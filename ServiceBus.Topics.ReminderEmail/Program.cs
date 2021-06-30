using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using ServiceBus.SharedLib;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Topics.ReminderEmail
{
    class Program
    {
        static ISubscriptionClient subscriptionClient;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            string serviceBusConnection = config["AzureServiceBusConnection"];
            string topicName = config["TopicName"];
            string subscriptionName = config["SubscriptioName"];


            subscriptionClient = new SubscriptionClient(serviceBusConnection, topicName, subscriptionName, ReceiveMode.PeekLock);

            var msgOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // No of messages service can process at time
                MaxConcurrentCalls = 1,

                //Indicates if service needs to wait untill message is fully proceessed.
                AutoComplete = false,
            };

            subscriptionClient.RegisterMessageHandler(SendEmailAsync, msgOptions);
            Console.ReadLine();
            await subscriptionClient.CloseAsync();
        }

        private static async Task SendEmailAsync(Message _message, CancellationToken _token)
        {
            //deserialize request/message from
            var message = Encoding.UTF8.GetString(_message.Body);
            SharedLib.PaymentReminderEmail email = JsonSerializer.Deserialize<SharedLib.PaymentReminderEmail>(message);
            await Helper.SendReminderEmail(email);
            await subscriptionClient.CompleteAsync(_message.SystemProperties.LockToken);

        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine($"Something went wrong, {args.Exception}");
            return Task.CompletedTask;
        }
    }
}
