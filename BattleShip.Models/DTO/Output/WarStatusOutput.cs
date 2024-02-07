namespace BattleShip.Models.DTO.Output;

public class WarStatusOutput
{
 
    public bool Over { get; set; }
    public Player winner { get; set; }
    
    public WarStatusOutput(War war)
    {
        Over = war.Seas.Any(s => s.Player.Ships.All(ship => ship.IsSunken()));
        winner = war.Seas.FirstOrDefault(s => s.Player.Ships.All(ship => ship.IsSunken()))?.Player;
    }
}