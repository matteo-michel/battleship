namespace BattleShip.Models;

public class Ship
{
    public Char Letter { get; set; }
    public int Size { get; set; }
    public Char Orientation { get; set; }
    public int[]? Position { get; set; }
    public int Hits { get; set; }
    
    public Ship(Char letter, int size, Char orientation)
    {
        Letter = letter;
        Size = size;
        Orientation = orientation;
        Hits = 0;
    }
    
    public bool IsSunken()
    {
        return Hits == Size;
    }
}