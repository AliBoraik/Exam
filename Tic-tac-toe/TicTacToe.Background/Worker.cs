namespace TicTacToe.Background;

public class Worker : BackgroundService
{
    private readonly Producer _producer;

    public Worker(Producer producer)
    {
        _producer = producer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        while (true)
        {
            _producer.SendMessage($"hi,{i}");
            Console.WriteLine("Worker send a message!!");
            await Task.Delay(5000);
            i++;
        }
    }
}