using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace TicTacToe.Background;

public class Producer
{
    private readonly string _queueName;

    public Producer()
    {
        _queueName = "TicTacToe";
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var messageJson = JsonSerializer.Serialize(message);
            
            var body = Encoding.UTF8.GetBytes(messageJson);

            channel.BasicPublish(exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body);
        }
    }
}