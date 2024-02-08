namespace BattleShip.Models.DTO.Input;

public class ShipInput
{
    public char Letter { get; set; }
    public int Size { get; set; }
    public char Orientation { get; set; }
    public int[][] Positions { get; set; }
}