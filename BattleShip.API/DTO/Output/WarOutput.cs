using BattleShip.Models;

namespace BattleShip.API.DTO.Output;

public class WarOutput
{
    public int Id { get; set; }
    public List<ShipOutput> Ships { get; } = new List<ShipOutput>();
    
    public WarOutput(War war)
    {
        Id = war.Id ?? 0;
        
        foreach (Ship ship in war.Seas[0].Pirate.Ships)
        {
            Ships.Add(new ShipOutput(ship));
        }
    }
}