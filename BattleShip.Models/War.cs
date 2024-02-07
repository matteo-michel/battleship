namespace BattleShip.Models;

public class War
{
    public int? Id { get; set; }
    public List<Sea> Seas { get; set; }
    
    public War(Pirate pirateA, Pirate pirateB)
    {
        Sea seaPirateA = new Sea(10, pirateA);
        Sea seaPirateB = new Sea(10, pirateB);
        Seas = new List<Sea> {seaPirateA, seaPirateB};
    }
}