
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using ServiceBus.SharedLib;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Queue
{
    class Program
    {

        static IQueueClient qClient;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json",true,true);
            var config = builder.Build();
            string serviceBusConnection = config["AzureServiceBusConnection"];
            string queueName = config["QueueName"];

            qClient = new QueueClient(serviceBusConnection, queueName);

            var msgOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // No of messages service can process at time
                MaxConcurrentCalls = 1,

                //Indicates if service needs to wait untill message is fully proceessed.
                AutoComplete = false,
            };

            qClient.RegisterMessageHandler(SendEmailAsync, msgOptions);
            Console.ReadLine();
            await qClient.CloseAsync();
        }

        private static async Task SendEmailAsync(Message _message, CancellationToken _token)
        {
            //deserialize request/message from
            var message = Encoding.UTF8.GetString(_message.Body);
            Email email = JsonSerializer.Deserialize<Email>(message);
            await SendEmail(email);
            await qClient.CompleteAsync(_message.SystemProperties.LockToken);

        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine($"Something went wrong, {args.Exception}");
            return Task.CompletedTask;
        }

        private static async Task SendEmail(Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("mukesh.kanojia.5053@gmail.com");
                mail.To.Add(email.EmailTo);
                mail.Subject = email.Subject;
                mail.Body = email.Body;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    //make sure to enable settings in gmail https://www.google.com/settings/security/lesssecureapps
                    smtp.Credentials = new NetworkCredential("username", "password");
                    smtp.EnableSsl = true;
                  await smtp.SendMailAsync(mail);
                }
               
                Console.WriteLine($"Email sent to {email.EmailTo}");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
