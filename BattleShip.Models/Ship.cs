namespace BattleShip.Models;

public class Ship
{
    public Char letter { get; set; }
    public int size { get; set; }
    public Char orientation { get; set; }
    
    public Ship(Char letter, int size, Char orientation)
    {
        this.letter = letter;
        this.size = size;
        this.orientation = orientation;
    }
}