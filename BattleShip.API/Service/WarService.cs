using BattleShip.Models;
using BattleShip.Models.DTO.Input;
using BattleShip.Models.DTO.Output;

namespace BattleShip.API.Service;

public class WarService
{
    public List<War> Wars { get; set; }
    
    public WarService()
    {
        Wars = new List<War>();
    }
    
    public War StartWar(string pirate, string navy)
    {
        War war = new War(new Pirate(pirate), new Navy(navy));
        Wars.Add(war);
        war.Id = Wars.IndexOf(war);
        return war;
    }
    
    public BlastOutput processBlast(int warId, int[] position)
    {
        War war = Wars[warId];
        Sea navySea = war.Seas[1];
        Ship? navyShip = navySea.Hit(position[0], position[1]);
        
        
        Navy navy = navySea.Player as Navy;
        
        if (navy is not Navy)
        {
            throw new Exception("Navy is not an instance of Navy");
        }
        
        int[] positionToAttack = navy.BlastLocations[0];
        navy.BlastLocations.RemoveAt(0);

        Sea pirateSea = war.Seas[0];
        Ship? pirateShip = pirateSea.Hit(positionToAttack[0], positionToAttack[1]);

        Console.WriteLine(war.Over);
        
        return new BlastOutput
        {
            Over = war.Over,
            Winner = war.Seas.FirstOrDefault(s => s.Player.Ships.All(ship => ship.IsSunken()))?.Player,
            Hit = navyShip is not null,
            Sunken = navyShip is not null && navyShip.IsSunken(),
            AiBlast = new AiBlastOutput()
            {
                X = positionToAttack[0],
                Y = positionToAttack[1],
                Hit = pirateShip is not null,
                Sunken = pirateShip is not null && pirateShip.IsSunken()
            }
        };
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