using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    /// <summary>
    /// 消息消费者Consumer
    /// </summary>
    public class Consumer
    {
        /// <summary>
        /// 消费消息
        /// </summary>
        public static void ReceiveMessage()
        {
            var factory = new ConnectionFactory();
            IConnection connection = null;

            factory.UserName = ConnectionFactory.DefaultUser;
            factory.Password = ConnectionFactory.DefaultPass;
            factory.VirtualHost = ConnectionFactory.DefaultVHost;
            factory.HostName = "127.0.0.1"; //设置RabbitMQ服务器所在的IP或主机名
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            connection = factory.CreateConnection();

            using (connection)
            {
                using (IModel channel = connection.CreateModel())
                {
                    //声明队列，主要为了防止消息接收者先运行此程序，队列还不存在时创建队列
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //创建事件驱动的消费者类型，尽量不要使用while(ture)循环来获取消息
                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model,ea)=> {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("我是消费者我接收到消息： {0}", message);
                    };

                    //指定消费队列
                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
                }
            }
        }
    }
}
