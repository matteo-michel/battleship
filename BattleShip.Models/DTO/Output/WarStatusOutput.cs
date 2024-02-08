namespace BattleShip.Models.DTO.Output;

public class WarStatusOutput
{
 
    public bool Over { get; set; }
    public Player? winner { get; set; }
    public Player? loser { get; set; }
}