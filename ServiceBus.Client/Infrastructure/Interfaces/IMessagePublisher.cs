using Microsoft.Azure.ServiceBus;
using ServiceBus.SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBus.Client.Infrastructure.Interfaces
{
    public interface IMessagePublisher
    {
         Task Queue<T>(T obj);
        Task PublishTopics<T>(T obj);


    }
}
