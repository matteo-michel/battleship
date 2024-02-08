namespace BattleShip.Models;

public class Pirate
{
    public string Name { get; set; }
    public bool isBot { get; set; } = false;
    public List<Ship> Ships { get; set; } = new List<Ship>();
    public List<int[]>? BlastLocations { get; set; }
}