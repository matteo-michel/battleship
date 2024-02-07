using BattleShip.API;
using BattleShip.API.DTO.Output;
using BattleShip.API.Service;
using BattleShip.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WarService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/war", (WarService warService) =>
{
    War war = warService.StartWar("Matteo", "Maid");
    warService.ToJaggedArray(war.Seas[0]);
    
    return new WarOutput(war);
});

app.MapPut("/game/{id}", (int id) =>
{
    // get body from request
    
    
    // var gameService = app.Services.GetRequiredService<GameService>();
    // Game game = gameService.GetGame(id);
    // return new GameOutput(game);
}).WithName("GetGame");

app.Run();
