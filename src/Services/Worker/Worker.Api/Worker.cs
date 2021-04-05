using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.Api
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "welcoming-mail";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = GetConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclarePassive(QueueName);
            _channel.BasicQos(0, 1, false);
            _logger.LogInformation($"Queue {QueueName} is waiting.");
            
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (obj, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());

                try
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        var emailModel = message.Deserialize<EmailModel>();

                        await Task.Delay(1000, stoppingToken);

                        _logger.LogInformation(
                            $"email: {emailModel.Email}, subject: {emailModel.Subject}, text: {emailModel.Text}");
                        _channel.BasicAck(args.DeliveryTag, false);
                    }
                    else
                    {
                        await Task.Delay(1000, stoppingToken);

                        _logger.LogInformation(
                            $"null message");
                        _channel.BasicAck(args.DeliveryTag, false);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"consumer error: {e.Message}");
                }
            };

            _channel.BasicConsume(QueueName, false, consumer);
            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("Connection closed.");
        }

        private IConnection GetConnection()
        {
            _connectionFactory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "host.docker.internal",
                Port = 5672,
                DispatchConsumersAsync = true
            };
            return _connectionFactory.CreateConnection();
        }
    }
}