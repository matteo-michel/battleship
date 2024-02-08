using BattleShip.API.Service;
using BattleShip.Models;
using BattleShip.Models.DTO.Input;
using BattleShip.Models.DTO.Output;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.API;

public static class WarController
{
    public static void RegisterWarController(this WebApplication app)
    {
        app.MapGet("/war", (WarService warService, SeaService seaService, PirateService pirateService) =>
        {
            Pirate pirate = pirateService.instanciatePirate("Matteo");
            Pirate navy = pirateService.instanciatePirate("Maid", true);
            
            Sea pirateSea = seaService.CreateSea(10, pirate);
            Sea navySea = seaService.CreateSea(10, navy);
            
            War war = warService.StartWar(pirateSea, navySea);
            warService.ToJaggedArray(war.Seas[0], war.Seas[1]);
    
            return new WarOutput
            {
                Id = war.Id ?? 0,
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
            int id,
            [FromBody] BlastInput position
        ) =>
        {
            
            War war = warService.Wars[id];
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

        // app.MapPut("/war/{id}", (
        //     WarService warService,
        //     SeaService seaService,
        //     int id,
        //     [FromBody] SeaInput seaInput
        //     ) =>
        // {
        //     War war = warService.Wars[id];
        //     // create temp sea to avoid changing the original one
        //     Sea tempSea = new Sea(war.Seas[0].Grid.Length, war.Seas[0].Player, false);
        //     
        //     // add the new ships in this temp sea and check they dont overlap. For each ship, get the letter and the size from the legacy ship
        //     foreach (ShipInput ship in seaInput.Ships)
        //     {
        //         Ship newShip = new Ship(ship.Letter, ship.Positions.Length, ship.Orientation);
        //         newShip.Position = ship.Positions[0];
        //         tempSea.AddShip(newShip);
        //     }
        //     
        // });
    }
}