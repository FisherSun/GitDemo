using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQProducer;
using RabbitMQConsumer;

namespace RabbitMQ.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer.SendMessage();

            Consumer.ReceiveMessage();
        }
    }
}
