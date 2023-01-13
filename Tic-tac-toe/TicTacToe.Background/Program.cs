using TicTacToe.Application;
using TicTacToe.Application.Configurations;
using TicTacToe.Background;
using TicTacToe.Background.Handlers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplication(hostContext.Configuration);
        services.AddTransient<Producer>();
        services.AddHostedService<Consumer>();
        services.AddHostedService<Worker>();
        services.AddHostedService<CacheHandler>();
    })
    .Build();

await host.RunAsync();