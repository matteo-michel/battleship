using BattleShip.Models;
using BattleShip.Models.DTO.Input;

namespace BattleShip.API.Service;

public class FleetService
{
    
    private SeaService SeaService { get; }
    private ShipService ShipService { get; }
    
    public FleetService(SeaService seaService, ShipService shipService)
    {
        SeaService = seaService;
        ShipService = shipService;
    }
    
    public Sea UpdateFleet(Sea sea, FleetInput fleetInput)
    {
        var tmpSea = SeaService.CloneSea(sea);
        
        foreach (var ship in fleetInput.Ships)
        {
            var newShip = ShipService.CreateShipFromShipInput(ship);
            tmpSea.Pirate.Ships.Add(newShip);
        }

        tmpSea = SeaService.FillSeaWithShips(tmpSea);

        return tmpSea;
    }
}