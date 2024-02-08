namespace BattleShip.Models;

public class Ship
{
    public Char Letter { get; set; }
    public int Size { get; set; }
    public Char Orientation { get; set; }
    public List<ShipPosition> Positions { get; set; } = new List<ShipPosition>();
    public int Hits { get; set; }
    
    public bool IsSunken()
    {
        return Hits == Size;
    }
}