using BattleShip.API.Service;
using BattleShip.Models;
using BattleShip.Models.DTO.Input;
using BattleShip.Models.DTO.Output;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WarService>();

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

app.MapGet("/war", (WarService warService) =>
{
    War war = warService.StartWar("Matteo", "Maid");
    warService.ToJaggedArray(war.Seas[0], war.Seas[1]);
    
    return new WarOutput
    {
        Id = war.Id ?? 0,
        Ships = war.Seas[0].Player.Ships.Select(s => 
            new ShipOutput
            {
                Letter = s.Letter.ToString(), 
                Positions = s.getPosition()
            }).ToList()
    };
});

app.MapPost("/war/blast/{id}", (
    WarService warService,
    int id,
    [FromBody] BlastInput position
    ) =>
{
    return warService.processBlast(id, new int[] { position.PosX, position.PosY });
});

app.MapGet("/war/status/{id}", (WarService warService, int id) =>
{
    War war = warService.Wars[id];
    return new WarStatusOutput
    {
        Over = war.Over,
        winner = war.Seas.FirstOrDefault(s => s.Player.Ships.All(ship => ship.IsSunken()))?.Player
    };
});

app.Run();
