using System.Drawing;
using BattleShip.API.Service;
using BattleShip.Models;
using BattleShip.Models.DTO.Enum;
using BattleShip.Models.DTO.Input;
using BattleShip.Models.DTO.Output;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.API;

public static class WarController
{
    public static void RegisterWarController(this WebApplication app)
    {
        app.MapPost("/war", (
            WarService warService,
            [FromBody] WarInput warInput
        ) =>
        {
            var war = warService.StartWar(warInput.Difficulty);
            warService.ToJaggedArray(war.Seas[0], war.Seas[1]);
            
            return new WarOutput
            {
                Id = war.Id ?? 0,
                gridSize = war.Seas[0].Size,
                Ships = war.Seas[0].Pirate.Ships.Select(s => 
                    new ShipOutput
                    {
                        Letter = s.Letter.ToString(), 
                        Positions = s.Positions.Select(p => 
                            new ShipPositionOutput
                            {
                                X = p.X,
                                Y = p.Y
                            }).ToList()
                    }).ToList()
            };
        });
        
        app.MapPost("/war/blast/{id}", (
            WarService warService,
            SeaService seaService,
            PirateService pirateService,
            int id,
            [FromBody] BlastInput position
        ) =>
        {
            var war = warService.Wars[id];
            
            if (seaService.IsHitInHistory(war.Seas[1], position.PosX, position.PosY))
            {
                throw new Exception("You already hit this position!");
            }
            
            Ship? navyShip = seaService.Hit(war.Seas[1], position.PosX, position.PosY);
            
            BlastOutput output = new BlastOutput()
            {
                Over = war.Over,
                Winner = war.Seas.FirstOrDefault(s => s.Pirate.Ships.All(ship => ship.IsSunken()))?.Pirate,
                Hit = navyShip is not null,
                Sunken = navyShip is not null && navyShip.IsSunken()
            };
            
            if (war.Seas[1].Pirate.isBot)
            {
                int[] positionToAttack = war.Seas[1].Pirate.BlastLocations![0];
                war.Seas[1].Pirate.BlastLocations?.RemoveAt(0);
                
                Ship? pirateShip = seaService.Hit(war.Seas[0], positionToAttack[0], positionToAttack[1]);
                
                if (pirateShip is not null && war.Difficulty == Difficulty.Hard)
                {
                    war.Seas[1].Pirate = (!pirateShip.IsSunken()
                        ? pirateService.ReorderBlastLocationsWithHitLocation(war.Seas[1].Pirate, positionToAttack[0],
                            positionToAttack[1])
                        : pirateService.reoderBlastLocation(war.Seas[1].Pirate));
                }
                
                output.AiBlast = new AiBlastOutput()
                {
                    X = positionToAttack[0],
                    Y = positionToAttack[1],
                    Hit = pirateShip is not null,
                    Sunken = pirateShip is not null && pirateShip.IsSunken()
                };
            }
            
            return output;
        });

        app.MapGet("/war/history/{id}", (
            WarService warService, 
            SeaService seaService, 
            int id,
            [FromQuery] int? wave
            )  =>
        {
            War war = warService.Wars[id];
            return new WarHistoryOutput
            {
                AllySea = seaService.GetSeaHistory(war.Seas[0], wave),
                EnemySea = seaService.GetSeaHistory(war.Seas[1], wave)
        
            };
        });
    }
}