using System.Drawing;

namespace BattleShip.Models;

public class Sea
{
    public Char[,] Grid { get; set; }
    public int Size { get; set; }
    public Pirate Pirate { get; set; }
    
    public List<Hit> Hits { get; set; } = new List<Hit>();
}