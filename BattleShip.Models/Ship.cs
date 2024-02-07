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

    public int[][] getPosition()
    {
        int[][] positions = new int[Size][];
        if (Orientation == 'H')
        {
            for (int i = 0; i < Size; i++)
            {
                positions[i] = new int[] {Position[0] + i, Position[1]};
            }
        }
        else
        {
            for (int i = 0; i < Size; i++)
            {
                positions[i] = new int[] {Position[0], Position[1] + i};
            }
        }

        return positions;
    }
        
}