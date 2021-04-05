using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Queue.Service
{
    public class RabbitMqService : IRabbitMqService
    {
        public void Publish(string message, string queueName)
        {
            Console.WriteLine($"publish:{message}:{queueName}");
            using var connection = GetConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);
            try
            {
                channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
                Console.WriteLine("published");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IConnection GetConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "rabbitmq",
                Port = 5672
            };
            return connectionFactory.CreateConnection();
        }
    }
}