using BattleShip.Models;

namespace BattleShip.API.DTO.Output;

public class GameOutput
{
    public int Id { get; set; }
    public List<ShipOutput> Ships { get; } = new List<ShipOutput>();
    
    public GameOutput(Game game)
    {
        Id = game.Id ?? 0;
        
        foreach (Ship ship in game.grids[0].player.ships)
        {
            Ships.Add(new ShipOutput(ship));
        }
    }
}