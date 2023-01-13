using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TicTacToe.Background;

public class Consumer : BackgroundService
{
    private IConnection? _connection;
    private IModel? _channel;
    private ConnectionFactory? _connectionFactory;


    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "TicTacToe",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        return base.StartAsync(cancellationToken); 
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<object>(body);
               // Console.WriteLine($"Consumer has message = {message}!");
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