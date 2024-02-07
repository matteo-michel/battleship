namespace BattleShip.Models;

public class Sea
{
    public Char[,] Grid { get; set; }
    public Pirate Pirate { get; set; }
    
    public Sea(int size, Pirate pirate)
    {
        Pirate = pirate;

        // fill Grid with \0
        Grid = new Char[size, size];
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Grid[i, j] = '\0';
            }
        }
        
        foreach (Ship ship in pirate.Ships)
        {
            bool placed = false;
            while (!placed)
            {
                int x = Random.Shared.Next(0, size);
                int y = Random.Shared.Next(0, size);
                if (ship.Orientation == 'H')
                {
                    if (x + ship.Size < size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.Size; i++)
                        {
                            if (Grid[x + i, y] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            ship.Position = new int[] {x, y};
                            for (int i = 0; i < ship.Size; i++)
                            {
                                Grid[x + i, y] = ship.Letter;
                            }
                            placed = true;
                        }
                    }
                }
                else
                {
                    if (y + ship.Size < size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.Size; i++)
                        {
                            if (Grid[x, y + i] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            ship.Position = new int[] {x, y};
                            for (int i = 0; i < ship.Size; i++)
                            {
                                Grid[x, y + i] = ship.Letter;
                            }
                            placed = true;
                        }
                    }
                }
            }
        }
    }
}