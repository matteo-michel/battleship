using BattleShip.Models;
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
        Sea pirateSea = war.Seas[0];
        Sea navySea = war.Seas[1];
        bool hit = false;
        bool sunken = false;
        foreach (Ship ship in navySea.Player.Ships)
        {
            if (ship.Position[0] == position[0] && ship.Position[1] == position[1])
            {
                hit = true;
                ship.Hits++;
                if (ship.Hits == ship.Size)
                {
                    sunken = true;
                }
            }
        }
        
        Navy navy = navySea.Player as Navy;
        
        // check that NavySea.Player is an instance of Navy
        if (navy is not Navy)
        {
            throw new Exception("Navy is not an instance of Navy");
        }
        
        // get the first element of navy's BlastPosition and then remove it
        int[] positionToAttack = navy.BlastLocations[0];
        navy.BlastLocations.RemoveAt(0);

        return new BlastOutput
        {
            Hit = hit,
            Sunken = sunken,
            Position = positionToAttack
        };
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