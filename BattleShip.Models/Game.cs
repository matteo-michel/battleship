namespace BattleShip.Models;

public class Game
{
    public int? Id { get; set; }
    public List<Board> grids { get; set; }
    
    public Game(Player playerA, Player playerB)
    {
        Board boardPlayerA = new Board(10, playerA);
        Board boardPlayerB = new Board(10, playerB);
        grids = new List<Board> {boardPlayerA, boardPlayerB};
    }
}