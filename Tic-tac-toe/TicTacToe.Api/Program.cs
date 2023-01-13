using TicTacToe.Application.Configurations;
using TicTacToe.Infrastructure;
using TicTacToe.Infrastructure.Configurations;
using TicTacToe.Interfaces;
using TicTacToeGame.Api;
using TicTacToeGame.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("gameHub");
app.Run();