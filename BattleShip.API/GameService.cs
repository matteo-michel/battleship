using BattleShip.Models;

namespace BattleShip.API;

public class GameService
{
    
    public List<Game> games { get; set; }
    
    public GameService()
    {
        this.games = new List<Game>();
    }
    
    public Game CreateGame(string playerA, string playerB)
    {
        Player player1 = new Player(playerA);
        Player player2 = new Player(playerB);
        Game game = new Game(player1, player2);
        games.Add(game);
        return game;
    }
    
    public Game GetGame(int id)
    {
        return this.games[id];
    }
    
    public char[][] ToJaggedArray(Board board)
    {
        Console.WriteLine();
        char[][] jaggedGrid = new char[10][];
        for (int i = 0; i < 10; i++)
        {
            jaggedGrid[i] = new char[10];
            for (int j = 0; j < 10; j++)
            {
                Console.Write((board.Grid[i, j] == '\0' ? "*" : board.Grid[i, j]) + " ");
                jaggedGrid[i][j] = board.Grid[i, j];
            }
            Console.WriteLine();
        }

        return jaggedGrid;
    }
}