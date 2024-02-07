namespace BattleShip.Models;

public class War
{
    public int? Id { get; set; }
    public List<Sea> Seas { get; set; }
    
    public War(Player pirate, Player navy)
    {
        Seas = new List<Sea> {new Sea(10, pirate), new Sea(10, navy)};
    }
}