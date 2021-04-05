
namespace Queue.Service
{
    public interface IRabbitMqService
    {
        void Publish(string message, string queueName);
    }
}