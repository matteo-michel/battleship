using BattleShip.Models;

namespace BattleShip.API.DTO.Output;

public class BoardOutput
{
    public List<ShipOutput> Ships { get; } = new List<ShipOutput>();
    
    public BoardOutput(Board board)
    {
        
        foreach (Ship ship in board.player.ships)
        {
            Ships.Add(new ShipOutput(ship));
        }
    }
}