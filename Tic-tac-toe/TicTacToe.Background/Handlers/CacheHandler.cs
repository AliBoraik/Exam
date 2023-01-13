using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TicTacToe.Interfaces;

namespace TicTacToe.Background.Handlers;

public class CacheHandler : BackgroundService
{
    private IConnection? _connection;
    private IModel? _channel;
    private ConnectionFactory? _connectionFactory;
    private readonly string _queueName;
    private ICacheService _cacheService;

    public CacheHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
        _queueName = "TicTacToe";
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        return base.StartAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<object>(body);
                if (message!= null)
                {
                    var cache = message.ToString()?.Split(",");
                    Console.WriteLine(message);
                    await _cacheService.SetData(cache[1], cache[0]);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        };
        _channel.BasicConsume(queue: "TicTacToe", autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }
}