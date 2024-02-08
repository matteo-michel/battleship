using BattleShip.Models;
using BattleShip.Models.DTO.Output;

namespace BattleShip.API.Service;

public class WarService
{
    public List<War> Wars { get; set; } = new List<War>();
    
    public War StartWar(Sea pirateSea, Sea navySea)
    {
        War war = new War() { Seas = new List<Sea> { pirateSea, navySea } };
        Wars.Add(war);
        war.Id = Wars.IndexOf(war);
        return war;
    }
    
    public char[][] ToJaggedArray(Sea sea, Sea sea2)
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
            Console.Write("  ");
            for (int j = 0; j < 10; j++)
            {
                Console.Write((sea2.Grid[i, j] == '\0' ? "*" : sea2.Grid[i, j]) + " ");
                jaggedGrid[i][j] = sea.Grid[i, j];
            }
            Console.WriteLine();
        }

        return jaggedGrid;
    }
    
    public static void diplayArray(int[][] array)
    {
        foreach (int[] row in array)
        {
            foreach (int cell in row)
            {
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }
}