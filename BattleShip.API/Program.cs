using System.Text.Json;
using BattleShip.API;
using BattleShip.API.DTO.Output;
using BattleShip.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<GameService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors("AllowAnyOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/game", () =>
{
    var gameService = app.Services.GetRequiredService<GameService>();
    Game game = gameService.CreateGame("Matteo", "Maid");

    gameService.ToJaggedArray(game.grids[0]);

    return new GameOutput(game);
});

app.MapGet("/game", () =>
{
    var gameService = app.Services.GetRequiredService<GameService>();
    return gameService.ToJaggedArray(gameService.games.Last().grids[0]);
});

app.MapGet("/game/{id}", (int id) =>
{
    var gameService = app.Services.GetRequiredService<GameService>();
    Game game = gameService.GetGame(id);

    return game;
});

app.Run();
