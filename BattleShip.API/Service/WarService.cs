using BattleShip.Models;

namespace BattleShip.API.Service;

public class WarService
{
    public List<War> Wars { get; set; }
    
    public WarService()
    {
        Wars = new List<War>();
    }
    
    public War StartWar(string pirateA, string pirateB)
    {
        Pirate pirate1 = new Pirate(pirateA);
        Pirate pirate2 = new Pirate(pirateB);
        War war = new War(pirate1, pirate2);
        Wars.Add(war);
        war.Id = Wars.IndexOf(war);
        return war;
    }
    
    public War findWar(int id)
    {
        return Wars[id];
    }
    
    public char[][] ToJaggedArray(Sea sea)
    {
        Console.WriteLine();
        char[][] jaggedGrid = new char[10][];
        for (int i = 0; i < 10; i++)
        {
            jaggedGrid[i] = new char[10];
            for (int j = 0; j < 10; j++)
            {
                Console.Write((sea.Grid[i, j] == '\0' ? "*" : sea.Grid[i, j]) + " ");
                jaggedGrid[i][j] = sea.Grid[i, j];
            }
            Console.WriteLine();
        }

        return jaggedGrid;
    }
}