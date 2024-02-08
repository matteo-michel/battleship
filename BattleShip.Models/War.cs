namespace BattleShip.Models;

public class War
{
    public int? Id { get; set; }
    public List<Sea> Seas { get; set; }
    public bool Over => Seas.Any(s => s.Pirate.Ships.All(ship => ship.IsSunken()));
}