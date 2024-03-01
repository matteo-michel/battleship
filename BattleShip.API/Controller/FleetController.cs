using BattleShip.API.Service;
using BattleShip.Models.DTO.Input;
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
    public Ok UpdateFleet(FleetInput fleetInput, string warId)
    {
        var war = WarService.Wars[int.Parse(warId)];
        war.Seas[0] = FleetService.UpdateFleet(war.Seas[0], fleetInput);
        
        return TypedResults.Ok();
    }
    
}