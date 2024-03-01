using BattleShip.Models;
using BattleShip.Models.DTO.Enum;

namespace BattleShip.API.Service;

public class WarService
{
    public List<War> Wars { get; set; } = new List<War>();
    private readonly SeaService _seaService;
    private readonly PirateService _pirateService;
    
    public WarService(SeaService seaService, PirateService pirateService)
    {
        _seaService = seaService;
        _pirateService = pirateService;
    }
    
    public War StartWar(Difficulty? difficulty)
    {
        var pirate = _pirateService.instanciatePirate("Matteo");
        var navy = _pirateService.instanciatePirate("Maid", true);
            
        var pirateSea = _seaService.CreateSea(GetSizebyDifficutly(difficulty), pirate);
        var navySea = _seaService.CreateSea(GetSizebyDifficutly(difficulty), navy);
        
        War war = new War()
        {
            Difficulty = difficulty ?? Difficulty.Hard, 
            Seas = new List<Sea> { pirateSea, navySea }
        };
        Wars.Add(war);
        war.Id = Wars.IndexOf(war);
        return war;
    }
    
    private static int GetSizebyDifficutly(Difficulty? difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return 8;
            case Difficulty.Hard:
                return 10;
            default:
                return 10;
        }
    }
    
    public char[][] ToJaggedArray(Sea sea, Sea sea2)
    {
        Console.WriteLine();
        char[][] jaggedGrid = new char[sea.Size][];
        for (int i = 0; i < sea.Size; i++)
        {
            jaggedGrid[i] = new char[sea.Size];
            for (int j = 0; j < sea.Size; j++)
            {
                Console.Write((sea.Grid[i, j] == '\0' ? "*" : sea.Grid[i, j]) + " ");
                jaggedGrid[i][j] = sea.Grid[i, j];
            }
            Console.Write("  ");
            for (int j = 0; j < sea.Size; j++)
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