namespace BattleShip.Models;

public class Board
{
    public Char[,] Grid { get; set; }
    public Player player { get; set; }
    
    public Board(int size, Player player)
    {

        // fill Grid with \0
        Grid = new Char[size, size];
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Grid[i, j] = '\0';
            }
        }
        
        foreach (Ship ship in player.ships)
        {
            bool placed = false;
            while (!placed)
            {
                int x = Random.Shared.Next(0, size);
                int y = Random.Shared.Next(0, size);
                if (ship.orientation == 'H')
                {
                    if (x + ship.size < size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.size; i++)
                        {
                            if (Grid[x + i, y] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int i = 0; i < ship.size; i++)
                            {
                                Grid[x + i, y] = ship.letter;
                            }
                            placed = true;
                        }
                    }
                }
                else
                {
                    if (y + ship.size < size)
                    {
                        bool canPlace = true;
                        for (int i = 0; i < ship.size; i++)
                        {
                            if (Grid[x, y + i] != '\0')
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace)
                        {
                            for (int i = 0; i < ship.size; i++)
                            {
                                Grid[x, y + i] = ship.letter;
                            }
                            placed = true;
                        }
                    }
                }
            }
        }
    }
}