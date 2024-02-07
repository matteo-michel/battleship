using System.Text.Json;
using BattleShip.API;
using BattleShip.API.DTO.Output;
using BattleShip.API.Service;
using BattleShip.Models;

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

app.MapPost("/war", (WarService warService) =>
{
    War war = warService.StartWar("Matteo", "Maid");
    warService.ToJaggedArray(war.Seas[0]);
    
    return new WarOutput(war);
});

app.Run();
