using BattleShip.API.Service;
using BattleShip.Models.DTO.Input;
using BattleShip.Models.DTO.Output;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.API;

[Route("fleet")]
[ApiController]
public class FleetController : Controller
{
    
    private WarService WarService { get; }
    private FleetService FleetService { get; }
    
    public FleetController(WarService warService, FleetService fleetService)
    {
        WarService = warService;
        FleetService = fleetService;
    }
    
    [HttpPut]
    [Route("{warId}")]
    public JsonHttpResult<WarOutput> UpdateFleet(FleetInput fleetInput, string warId)
    {
        var war = WarService.Wars[int.Parse(warId)];
        war.Seas[0] = FleetService.UpdateFleet(war.Seas[0], fleetInput);

        return TypedResults.Json(new WarOutput
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
        });
    }
    
}